namespace WeappyTest
{
    public class NavigatePageCommand<T> : ICommand where T : BasePage
    {
        private NavigationService _navigationService;
        private T _page;

        public NavigatePageCommand(NavigationService navigationService, T page)
        {
            _navigationService = navigationService;
            _page = page;
        }

        public void Execute()
        {
            _navigationService.Navigate(_page);
        }
    }
}
