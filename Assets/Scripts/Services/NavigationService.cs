namespace WeappyTest
{
    [ZenjectBindingAsSingle]
    public class NavigationService
    {
        private BasePage _currentPage;

        public void Navigate<T>(T page) where T : BasePage
        {
            if (page == null || _currentPage == page)
                return;
            if (_currentPage != null)
                _currentPage.gameObject.SetActive(false);
            _currentPage = page;
            _currentPage.gameObject.SetActive(true);
        }
    }
}
