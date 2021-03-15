using UnityEngine.SceneManagement;
using Zenject;

namespace WeappyTest.Character
{
    public class DieState : CharacterState
    {
        [Inject]
        private NavigatePageCommand<MenuPage> _menuPageCommand;
        [Inject]
        private FadeService _fadeService;

        protected override void OnEnter(CharacterContext context)
        {
            context.Animator.SetTrigger("Fall");
            context.VerticalSpeed = 0f;
            context.VerticalAcceleration = 0f;
            context.HorizontalSpeed = 0f;

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
