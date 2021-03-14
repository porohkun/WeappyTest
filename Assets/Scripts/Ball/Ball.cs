using System;
using UnityEngine;

namespace WeappyTest.Ball
{
    public class Ball : Actor<BallContext>
    {
        [SerializeField]
        private MyBoxCollider2D _additionalCollider;

        protected override BallContext CreateContext()
        {
            return new BallContext(_spriteRenderer, _animator);
        }

        protected override void ConfigureBehaviour()
        {
            _states.AddState<IdleState>();
            _states.AddState<FlyState>();
            _states.AddState<JumpState>();
            _states.AddState<FallState>();
            _states.AddState<BounceState>();

            //_states.AddTransition<IdleState, RunState>(c => InputWrapper.Left ^ InputWrapper.Right);
            //_states.AddTransition<IdleState, JumpState>(c => InputWrapper.BeginJump);
            //_states.AddTransition<IdleState, FallState>(c => !c.TouchFloor);

            //_states.AddTransition<RunState, IdleState>(c => InputWrapper.Left == InputWrapper.Right);
            //_states.AddTransition<RunState, JumpState>(c => InputWrapper.BeginJump);
            //_states.AddTransition<RunState, FallState>(c => !c.TouchFloor);

            //_states.AddTransition<JumpState, FallState>(c => !InputWrapper.Jump);
            //_states.AddTransition<JumpState, FallState>(c => c.VerticalSpeed <= 0f);

            //_states.AddTransition<FallState, IdleState>(c => c.TouchFloor);
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
            _additionalCollider.enabled = false;
        }

        //protected override void CheckCollisions()
        //{
        //foreach (var collision in _myPhysicsService.GetIntersections(_collider))
        //    if (collision.tag == "Floor")
        //    {
        //        _context.TouchFloor = true;
        //        transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
        //    }
        //    else if (collision.tag == "Wall")
        //    {
        //        _context.HorizontalSpeed = 0;
        //        if (_context.Direction == Direction.Left)
        //            transform.position += new Vector3(collision.Bounds.xMax - _collider.Bounds.xMin, 0f);
        //        else
        //            transform.position += new Vector3(collision.Bounds.xMin - _collider.Bounds.xMax, 0f);
        //    }
        // }

        protected override void CheckCollisionsHorizontal()
        {

        }

        protected override void CheckCollisionsVertical()
        {

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

        }
    }
}
