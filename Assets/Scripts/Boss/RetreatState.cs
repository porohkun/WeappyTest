using UnityEngine.SceneManagement;
using Zenject;

namespace WeappyTest.Boss
{
    public class RetreatState : BossState
    {
        [Inject]
        private NavigatePageCommand<MenuPage> _menuPageCommand;
        [Inject]
        private FadeService _fadeService;

        protected override void OnEnter(BossContext context)
        {
            context.HorizontalSpeed = _settings.RetreatHorizontalSpeed;
            context.VerticalSpeed = _settings.RetreatVerticalSpeed;

            InputWrapper.Enabled = false;
            SetDelay(1.5f, () =>
            {
                _fadeService.FadeOut(() =>
                {
                    var scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                });
            });
        }
    }
}
