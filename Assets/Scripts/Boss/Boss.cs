using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeappyTest.Boss
{
    public class Boss : Actor<BossContext>
    {
        [SerializeField]
        private BossSpawnPoint[] _leftPoints;
        [SerializeField]
        private BossSpawnPoint[] _rightPoints;
        [SerializeField]
        private SlimeSpawnPoint[] _slimeSpawnPoints;

        public IEnumerable<SlimeSpawnPoint> SlimeSpawnPoints => _slimeSpawnPoints;

        [Inject]
        private Settings _settings { get; set; }
        [Inject]
        private Slime.Slime.Factory _slimeFactory { get; set; }

        protected override BossContext CreateContext()
        {
            return new BossContext(this, _effects, _spriteRenderer, _animator) { Lives = _settings.Lives };
        }

        public void SpawnSlimeAt(SlimeSpawnPoint point)
        {
            var slime = _slimeFactory.Create();
            slime.transform.SetParent(transform.parent.parent);
            slime.transform.position = new Vector3(point.transform.position.x, transform.position.y);
            slime.Context.Direction = point.Direction; ;
        }

        public bool TeleportToSpawner(int spawnerIndex)
        {
            var spawner = (Context.Direction == Direction.Left ? _rightPoints : _leftPoints)[spawnerIndex];
            transform.position = spawner.Position;
            return spawner.LowFlight;
        }

        protected override void ConfigureBehaviour()
        {
            _states.AddState<HighFlightState>();
            _states.AddState<LowFlightState>();
            _states.AddState<FlipState>();
            _states.AddState<PauseState>();
            _states.AddState<HitState>();
            _states.AddState<RetreatState>();


            _states.AddTransition<HighFlightState, FlipState>(c => c.TouchSpawner);

            _states.AddTransition<LowFlightState, FlipState>(c => c.TouchSpawner);

            _states.AddTransition<FlipState, PauseState>(0f);

            _states.AddTransition<PauseState, HighFlightState>(c => c.Ready && c.HighFlightReady);

            _states.AddTransition<PauseState, LowFlightState>(c => c.Ready && c.LowFlightReady);

            _states.AddTransition<HitState, HighFlightState>(c => c.Lives > 0 && c.Ready && c.HighFlightReady);

            _states.AddTransition<HitState, LowFlightState>(c => c.Lives > 0 && c.Ready && c.LowFlightReady);

            _states.AddTransition<HitState, RetreatState>(c => c.Lives == 0);
        }

        protected override void CheckCollisionsHorizontal()
        {
            Context.TouchSpawner = false;
            foreach (var collision in _myPhysicsService.GetIntersections(_collider))
                if (collision.tag == "Spawner")
                {
                    Context.TouchSpawner = true;
                }
                else if (collision.tag == "Player")
                {
                    var player = collision.GetComponent<Character.Character>();
                    if (!player.Context.Blinking)
                    {
                        player.Context.Direction = (Direction)(-(int)Context.Direction);
                        player.TriggerState<Character.HitState>();
                    }
                }
                else if (collision.tag == "BallH")
                {
                    if (!Context.Blinking && collision.GetComponent<Ball.Ball>().Context.IsProjectile)
                        TriggerState<HitState>();
                }
        }

        protected override void CheckCollisionsVertical()
        {

        }

        protected override void OnUpdate()
        {
            Context.Animator.SetBool("Blink", Context.Blinking);

            DebugViewer.AddValue("Blinking", Context.Blinking);
        }

        [Serializable]
        public class Settings
        {
            [SerializeField]
            private int _lives = 5;
            public int Lives => _lives;

            [SerializeField]
            private float _highFlightSpeed = 0.75f;
            public float HighFlightSpeed => _highFlightSpeed;

            [SerializeField]
            private float _lowFlightSpeed = 0.75f;
            public float LowFlightSpeed => _lowFlightSpeed;

            [SerializeField]
            private float _minPauseLength = 0.75f;
            public float MinPauseLength => _minPauseLength;

            [SerializeField]
            private float _maxPauseLength = 0.75f;
            public float MaxPauseLength => _maxPauseLength;

            [SerializeField]
            private float _blinkTime = 0.75f;
            public float BlinkTime => _blinkTime;

            [SerializeField]
            private float _retreatHorizontalSpeed = 0.75f;
            public float RetreatHorizontalSpeed => _retreatHorizontalSpeed;

            [SerializeField]
            private float _retreatVerticalSpeed = 0.75f;
            public float RetreatVerticalSpeed => _retreatVerticalSpeed;
        }
    }
}
