﻿using Zenject;

namespace WeappyTest.Ball
{
    public abstract class BallState : BaseState<BallContext>
    {
        protected Ball.Settings _settings { get; private set; }

        [Inject]
        public void Inject(Ball.Settings settings)
        {
            _settings = settings;
        }
    }
}
