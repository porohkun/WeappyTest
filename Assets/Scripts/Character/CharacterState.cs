using Zenject;

namespace WeappyTest
{
    public abstract class CharacterState : IState<CharacterContext>
    {
        protected Character.Settings _settings { get; private set; }

        [Inject]
        public void Inject(Character.Settings settings)
        {
            _settings = settings;
        }

        public abstract void OnEnter(CharacterContext context);
        public abstract void OnExit(CharacterContext context);
        public abstract void Update(CharacterContext context);
    }
}
