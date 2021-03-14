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
            return new CharacterContext(this, _spriteRenderer, _animator);
        }

        public void ThrowBallUp()
        {
            CarryBall.TriggerState<Ball.JumpState>();
            CarryBall.EnableCollider();
            CarryBall = null;
        }

        public void ThrowBall()
        {
            CarryBall.transform.position = transform.position + _throwOffset.Scale((int)_context.Direction, 1f).ToVector3();
            if (_context.Direction == Direction.Right)
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

            _states.AddTransition<IdleState, RunState>(c => InputWrapper.Left ^ InputWrapper.Right);
            _states.AddTransition<IdleState, JumpState>(c => InputWrapper.BeginJump);
            _states.AddTransition<IdleState, FallState>(c => !c.TouchFloor);
            _states.AddTransition<IdleState, ThrowState>(c => c.CarryBall && InputWrapper.BeginGrab && !InputWrapper.Up);
            _states.AddTransition<IdleState, ThrowUpState>(c => c.CarryBall && InputWrapper.BeginGrab && InputWrapper.Up);

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
        }

        protected override void CheckCollisionsHorizontal()
        {
            _context.TouchingBall = null;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Wall" || collision.tag == "BallH")
                {
                    if (collision.tag == "BallH")
                        _context.TouchingBall = collision.GetComponent<Ball.Ball>();
                    _context.HorizontalSpeed = 0;
                    if (_context.Direction == Direction.Left)
                        transform.position += new Vector3(collision.Bounds.xMax - _collider.Bounds.xMin, 0f);
                    else
                        transform.position += new Vector3(collision.Bounds.xMin - _collider.Bounds.xMax, 0f);
                }
        }

        protected override void CheckCollisionsVertical()
        {
            _context.TouchFloor = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Floor" || collision.tag == "BallV")
                {
                    //if (!_context.TouchFloor)
                    transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
                    _context.TouchFloor = true;
                }
        }

        protected override void OnUpdate()
        {
            if (CarryBall != null)
                CarryBall.transform.position = transform.position + _carryOffset.ToVector3();
        }

        [Serializable]
        public class Settings
        {
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
        }
    }
}
