using Zenject;

namespace WeappyTest.Slime
{
    public abstract class SlimeState : BaseState<SlimeContext>
    {
        protected Slime.Settings _settings { get; private set; }

        [Inject]
        public void Inject(Slime.Settings settings)
        {
            _settings = settings;
        }
    }
}
