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
            _states.AddState<ThrowedRightState>();
            _states.AddState<ThrowedLeftState>();
            _states.AddState<JumpState>();
            _states.AddState<FlyState>();
            _states.AddState<TouchWallState>();
            _states.AddState<FallState>();
            _states.AddState<BounceState>();

            _states.AddTransition<ThrowedLeftState, FlyState>(0f);

            _states.AddTransition<ThrowedRightState, FlyState>(0f);

            _states.AddTransition<JumpState, FallState>(c => c.TouchCeiling);

            _states.AddTransition<FlyState, TouchWallState>(c => c.TouchWall);

            _states.AddTransition<TouchWallState, FlyState>(c => c.WallTouchesLeft > 0);
            _states.AddTransition<TouchWallState, FallState>(c => c.WallTouchesLeft <= 0);

            _states.AddTransition<FallState, BounceState>(c => c.TouchFloor);

            _states.AddTransition<BounceState, IdleState>(c => c.TouchFloor);

        }

        public void DisableCollider()
        {
            _collider.enabled = false;
            _additionalCollider.enabled = false;
        }

        public void EnableCollider()
        {
            _collider.enabled = true;
            _additionalCollider.enabled = true;
        }

        protected override void CheckCollisionsHorizontal()
        {
            _context.TouchWall = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Wall")
                {
                    _context.TouchWall = true;
                    if (_context.Direction == Direction.Left)
                        transform.position += new Vector3(collision.Bounds.xMax - _collider.Bounds.xMin, 0f);
                    else
                        transform.position += new Vector3(collision.Bounds.xMin - _collider.Bounds.xMax, 0f);
                }
        }

        protected override void CheckCollisionsVertical()
        {
            _context.TouchFloor = false;
            _context.TouchCeiling = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Floor")
                {
                    transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
                    _context.TouchFloor = true;
                }
                else if (collision.tag == "Ceiling")
                {
                    transform.position += new Vector3(0f, collision.Bounds.yMin - _collider.Bounds.yMax);
                    _context.TouchCeiling = true;
                }
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private float _horizontalSpeed = 0.75f;
            public float HorizontalSpeed => _horizontalSpeed;

            [SerializeField]
            private float _verticalSpeed = 0.75f;
            public float VerticalSpeed => _verticalSpeed;

            [SerializeField]
            private float _fallSpeed = 0.75f;
            public float FallSpeed => _fallSpeed;

            [SerializeField]
            private float _bounceSpeed = 0.75f;
            public float BounceSpeed => _bounceSpeed;

            [SerializeField]
            private float _bounceAcceleration = 0.75f;
            public float BounceAcceleration => _bounceAcceleration;

        }
    }
}
