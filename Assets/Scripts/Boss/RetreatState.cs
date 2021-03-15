using Zenject;

namespace WeappyTest.Boss
{
    public class RetreatState : BossState
    {
        [Inject]
        private NavigatePageCommand<MenuPage> _menuPageCommand;

        protected override void OnEnter(BossContext context)
        {
            context.HorizontalSpeed = _settings.RetreatHorizontalSpeed;
            context.VerticalSpeed = _settings.RetreatVerticalSpeed;

            SetDelay(1.5f, () => _menuPageCommand.Execute());
        }
    }
}
