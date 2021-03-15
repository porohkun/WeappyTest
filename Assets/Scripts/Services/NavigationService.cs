using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [ZenjectBindingAsSingle]
    public class NavigationService
    {
        private FadeService _fadeService;
        private BasePage _currentPage;

        [Inject]
        private void Inject(FadeService fadeService)
        {
            _fadeService = fadeService;
        }

        public void Navigate<T>(T page) where T : BasePage
        {
            if (page == null || _currentPage == page)
                return;
            InputWrapper.Enabled = false;
            if (_currentPage != null)
                _fadeService.FadeOut(() =>
                {
                    _currentPage.gameObject.SetActive(false);
                    ShowNewPage(page);
                });
            else
                ShowNewPage(page);
        }

        private void ShowNewPage<T>(T page) where T : BasePage
        {
            _currentPage = page;
            _currentPage.gameObject.SetActive(true);
            _currentPage.StartCoroutine(PauseRoutine(0.3f, () => _fadeService.FadeIn(() => InputWrapper.Enabled = true)));

        }

        IEnumerator PauseRoutine(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }

    }
}
