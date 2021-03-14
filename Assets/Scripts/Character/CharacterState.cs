using Zenject;

namespace WeappyTest.Character
{
    public abstract class CharacterState : BaseState<CharacterContext>
    {
        protected Character.Settings _settings { get; private set; }

        [Inject]
        public void Inject(Character.Settings settings)
        {
            _settings = settings;
        }
    }
}
