using System;
using UnityEngine;
using Zenject;

namespace WeappyTest.Character
{
    public class Character : Actor<CharacterContext>
    {
        [SerializeField]
        private Vector2 _carryOffset;
        [SerializeField]
        private Vector2 _throwOffset;

        [Inject]
        private Settings _settings { get; set; }
        public Ball.Ball CarryBall { get; internal set; }

        protected override CharacterContext CreateContext()
        {
            return new CharacterContext(this, _effects, _spriteRenderer, _animator) { Lives = _settings.Lives };
        }

        public void DropBall()
        {
            CarryBall.TriggerState<Ball.BounceState>();
            CarryBall.EnableCollider();
            CarryBall = null;
        }

        public void ThrowBallUp()
        {
            CarryBall.TriggerState<Ball.JumpState>();
            CarryBall.EnableCollider();
            CarryBall = null;
        }

        public void ThrowBall()
        {
            CarryBall.transform.position = transform.position + _throwOffset.Scale((int)Context.Direction, 1f).ToVector3();
            if (Context.Direction == Direction.Right)
                CarryBall.TriggerState<Ball.ThrowedRightState>();
            else
                CarryBall.TriggerState<Ball.ThrowedLeftState>();
            CarryBall.EnableCollider();
            CarryBall = null;
        }

        public void PickUpBall(Ball.Ball ball)
        {
            CarryBall = ball;
            CarryBall.TriggerState<Ball.IdleState>();
            ball.DisableCollider();
        }

        protected override void ConfigureBehaviour()
        {
            _states.AddState<IdleState>();
            _states.AddState<RunState>();
            _states.AddState<JumpState>();
            _states.AddState<FallState>();
            _states.AddState<LandingState>();
            _states.AddState<PickUpState>();
            _states.AddState<ThrowState>();
            _states.AddState<ThrowUpState>();
            _states.AddState<StunState>();
            _states.AddState<HitState>();
            _states.AddState<DieState>();

            _states.AddTransition<IdleState, RunState>(c => InputWrapper.Left ^ InputWrapper.Right);
            _states.AddTransition<IdleState, JumpState>(c => InputWrapper.BeginJump);
            _states.AddTransition<IdleState, FallState>(c => !c.TouchFloor);
            _states.AddTransition<IdleState, ThrowState>(c => c.CarryBall && InputWrapper.BeginGrab && !InputWrapper.Up);
            _states.AddTransition<IdleState, ThrowUpState>(c => c.CarryBall && InputWrapper.BeginGrab && InputWrapper.Up);
            _states.AddTransition<IdleState, DieState>(c => c.Dead);

            _states.AddTransition<RunState, IdleState>(c => InputWrapper.Left == InputWrapper.Right);
            _states.AddTransition<RunState, JumpState>(c => InputWrapper.BeginJump);
            _states.AddTransition<RunState, FallState>(c => !c.TouchFloor);
            _states.AddTransition<RunState, PickUpState>(c => c.TouchBall && InputWrapper.BeginGrab);
            _states.AddTransition<RunState, ThrowState>(c => c.CarryBall && InputWrapper.BeginGrab && !InputWrapper.Up);
            _states.AddTransition<RunState, ThrowUpState>(c => c.CarryBall && InputWrapper.BeginGrab && InputWrapper.Up);

            _states.AddTransition<JumpState, FallState>(c => !InputWrapper.Jump);
            _states.AddTransition<JumpState, FallState>(c => c.VerticalSpeed <= 0f);
            _states.AddTransition<JumpState, ThrowState>(c => c.CarryBall && InputWrapper.BeginGrab && !InputWrapper.Up);
            _states.AddTransition<JumpState, ThrowUpState>(c => c.CarryBall && InputWrapper.BeginGrab && InputWrapper.Up);

            _states.AddTransition<FallState, LandingState>(c => c.TouchFloor && !c.CarryBall);
            _states.AddTransition<FallState, IdleState>(c => c.TouchFloor && c.CarryBall);
            _states.AddTransition<FallState, ThrowState>(c => c.CarryBall && InputWrapper.BeginGrab && !InputWrapper.Up);
            _states.AddTransition<FallState, ThrowUpState>(c => c.CarryBall && InputWrapper.BeginGrab && InputWrapper.Up);

            _states.AddTransition<LandingState, IdleState>(c => InputWrapper.Jump || (InputWrapper.Left ^ InputWrapper.Right));
            _states.AddTransition<LandingState, IdleState>(_settings.LandingTime);

            _states.AddTransition<PickUpState, IdleState>(_settings.PickUpTime);

            _states.AddTransition<ThrowState, IdleState>(_settings.ThrowTime);

            _states.AddTransition<ThrowUpState, IdleState>(_settings.ThrowTime);

            _states.AddTransition<StunState, IdleState>(_settings.StunTime);

            _states.AddTransition<HitState, IdleState>(_settings.FallTime);
        }

        protected override void CheckCollisionsHorizontal()
        {
            if (Context.Dead)
                return;
            Context.TouchingBall = null;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Wall" || collision.tag == "BallH")
                {
                    if (collision.tag == "BallH")
                        if (!collision.GetComponent<Ball.Ball>().Context.IsProjectile)
                            Context.TouchingBall = collision.GetComponent<Ball.Ball>();
                        else
                        {
                            if (!Context.Blinking)
                                TriggerState<StunState>();
                            continue;
                        }
                    Context.HorizontalSpeed = 0;
                    if (collision.Bounds.center.x < _collider.Bounds.center.x)
                        transform.position += new Vector3(collision.Bounds.xMax - _collider.Bounds.xMin, 0f);
                    else
                        transform.position += new Vector3(collision.Bounds.xMin - _collider.Bounds.xMax, 0f);
                }
        }

        protected override void CheckCollisionsVertical()
        {
            if (Context.Dead)
                return;
            Context.TouchFloor = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Floor" || collision.tag == "BallV")
                {
                    if (collision.tag == "BallV" && collision.transform.parent.GetComponent<Ball.Ball>().Context.IsProjectile)
                    {
                        continue;
                    }
                    transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
                    Context.TouchFloor = true;
                }
        }

        protected override void OnUpdate()
        {
            if (CarryBall != null)
                CarryBall.transform.position = transform.position + _carryOffset.ToVector3();

            Context.Animator.SetBool("Blink", Context.Blinking);
            Context.Animator.SetBool("Carry", Context.CarryBall);

            //DebugViewer.AddValue("Blinking", Context.Blinking);
            //DebugViewer.AddValue("Direction", Context.Direction);
            //DebugViewer.AddValue("HorizontalSpeed", Context.HorizontalSpeed);
            //DebugViewer.AddValue("VerticalSpeed", Context.VerticalSpeed);
            //DebugViewer.AddValue("VerticalAcceleration", Context.VerticalAcceleration);
            //DebugViewer.AddValue("CarryBall", Context.CarryBall);
            //DebugViewer.AddValue("TouchBall", Context.TouchBall);
            //DebugViewer.AddValue("TouchFloor", Context.TouchFloor);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private Character[] _prefabs;
            public Character[] Prefabs => _prefabs;

            [SerializeField]
            private int _lives = 3;
            public int Lives => _lives;

            [SerializeField]
            private float _runSpeed = 0.75f;
            public float RunSpeed => _runSpeed;

            [SerializeField]
            private float _flySpeed = 0.75f;
            public float FlySpeed => _flySpeed;

            [SerializeField]
            private float _jumpSpeed = 2;
            public float JumpSpeed => _jumpSpeed;

            [SerializeField]
            private float _jumpGravity = -5;
            public float JumpGravity => _jumpGravity;

            [SerializeField]
            private float _fallGravity = -5;
            public float FallGravity => _fallGravity;

            [SerializeField]
            private float _landingTime = 0.5f;
            public float LandingTime => _landingTime;

            [SerializeField]
            private float _pickUpTime = 0.2f;
            public float PickUpTime => _pickUpTime;

            [SerializeField]
            private float _throwTime = 0.2f;
            public float ThrowTime => _throwTime;

            [SerializeField]
            private float _throwUpVerticalSpeed = 0.2f;
            public float ThrowUpVerticalSpeed => _throwUpVerticalSpeed;

            [SerializeField]
            private float _throwUpVerticalAcceleration = 0.2f;
            public float ThrowUpVerticalAcceleration => _throwUpVerticalAcceleration;

            [SerializeField]
            private float _stunVerticalSpeed = 0.2f;
            public float StunVerticalSpeed => _stunVerticalSpeed;

            [SerializeField]
            private float _stunVerticalAcceleration = 0.2f;
            public float StunVerticalAcceleration => _stunVerticalAcceleration;

            [SerializeField]
            private float _stunTime = 0.2f;
            public float StunTime => _stunTime;

            [SerializeField]
            private float _stunBlinkTime = 0.2f;
            public float StunBlinkTime => _stunBlinkTime;

            [SerializeField]
            private float _fallHorizontalSpeed = 0.2f;
            public float FallHorizontalSpeed => _fallHorizontalSpeed;

            [SerializeField]
            private float _fallVerticalSpeed = 0.2f;
            public float FallVerticalSpeed => _fallVerticalSpeed;

            [SerializeField]
            private float _fallVerticalAcceleration = 0.2f;
            public float FallVerticalAcceleration => _fallVerticalAcceleration;

            [SerializeField]
            private float _fallTime = 0.2f;
            public float FallTime => _fallTime;

            [SerializeField]
            private float _fallBlinkTime = 0.2f;
            public float FallBlinkTime => _fallBlinkTime;
        }

        public class FactoryChip : PlaceholderFactory<Character> { }
        public class FactoryDale : PlaceholderFactory<Character> { }
    }
}
