using Zenject;

namespace WeappyTest.Character
{
    public class DieState : CharacterState
    {
        [Inject]
        private NavigatePageCommand<MenuPage> _menuPageCommand;

        protected override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Fall");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;

            SetDelay(1.5f, () => _menuPageCommand.Execute());
        }
    }
}
