using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [ZenjectBindingInstanceAsSingle]
    public class MenuPage : BasePage
    {
        [SerializeField]
        private GameObject[] _charactersOutline;

        private int _selectedCharacter = 0;
        private ICommand _navigateGamePageCommand;

        [Inject]
        void Inject(NavigatePageCommand<GamePage> navigateGamePageCommand)
        {
            _navigateGamePageCommand = navigateGamePageCommand;
        }

        void Start()
        {
            ShowCharacter();
        }

        void Update()
        {
            if (InputWrapper.BeginLeft || InputWrapper.BeginRight)
            {
                _selectedCharacter = (_selectedCharacter + 1) % _charactersOutline.Length;
                ShowCharacter();
            }

            if (InputWrapper.BeginEnter)
                _navigateGamePageCommand.Execute();
        }

        private void ShowCharacter()
        {
            for (int i = 0; i < _charactersOutline.Length; i++)
                _charactersOutline[i].SetActive(i == _selectedCharacter);
        }
    }
}
