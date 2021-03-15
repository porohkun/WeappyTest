using Zenject;

namespace WeappyTest.Boss
{
    public abstract class BossState : BaseState<BossContext>
    {
        protected Boss.Settings _settings { get; private set; }

        [Inject]
        public void Inject(Boss.Settings settings)
        {
            _settings = settings;
        }
    }
}
