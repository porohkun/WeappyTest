using System;
using UnityEngine;
using Zenject;

namespace WeappyTest.Slime
{
    public class Slime : Actor<SlimeContext>
    {
        [Inject]
        private Settings _settings { get; set; }

        protected override SlimeContext CreateContext()
        {
            return new SlimeContext(_effects, _spriteRenderer, _animator);
        }

        protected override void ConfigureBehaviour()
        {
            _states.AddState<FallState>();
            _states.AddState<BounceState>();
            _states.AddState<RunState>();
            _states.AddState<DieState>();


            _states.AddTransition<FallState, BounceState>(c => c.TouchFloor);

            _states.AddTransition<BounceState, RunState>(c => c.TouchFloor);

        }

        protected override void CheckCollisionsHorizontal()
        {
            if (Context.Dead)
                return;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Player")
                {
                    var player = collision.GetComponent<Character.Character>();
                    if (!player.Context.Blinking)
                    {
                        player.Context.Direction = (Direction)(-(int)Context.Direction);
                        player.TriggerState<Character.HitState>();
                    }
                }
                else if (collision.tag == "BallH" && collision.GetComponent<Ball.Ball>().Context.IsProjectile)
                {
                    TriggerState<DieState>();
                }
        }

        protected override void CheckCollisionsVertical()
        {
            if (Context.Dead)
                return;
            Context.TouchFloor = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Floor")
                {
                    transform.position += new Vector3(0f, collision.Bounds.yMax - _collider.Bounds.yMin);
                    Context.TouchFloor = true;
                }
        }

        protected override void OnUpdate()
        {
            if (transform.position.y < _settings.DestroyDepth)
                Destroy(gameObject);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private Slime _prefab;
            public Slime Prefab => _prefab;

            [SerializeField]
            private float _fallSpeed = 0.75f;
            public float FallSpeed => _fallSpeed;

            [SerializeField]
            private float _fallAcceleration = 0.75f;
            public float FallAcceleration => _fallAcceleration;

            [SerializeField]
            private float _bounceSpeed = 0.75f;
            public float BounceSpeed => _bounceSpeed;

            [SerializeField]
            private float _bounceAcceleration = 0.75f;
            public float BounceAcceleration => _bounceAcceleration;

            [SerializeField]
            private float _runSpeed = 0.75f;
            public float RunSpeed => _runSpeed;

            [SerializeField]
            private float _dieSpeed = 0.75f;
            public float DieSpeed => _dieSpeed;

            [SerializeField]
            private float _dieAcceleration = 0.75f;
            public float DieAcceleration => _dieAcceleration;

            [SerializeField]
            private float _destroyDepth = 0.75f;
            public float DestroyDepth => _destroyDepth;
        }

        public class Factory : PlaceholderFactory<Slime> { }
    }
}
