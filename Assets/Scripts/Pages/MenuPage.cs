using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [ZenjectBindingInstanceAsSingle]
    public class MenuPage : BasePage
    {
        [SerializeField]
        private SpriteRenderer[] _charactersOutline;

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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _selectedCharacter = (_selectedCharacter + 1) % _charactersOutline.Length;
                ShowCharacter();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                _navigateGamePageCommand.Execute();
        }

        private void ShowCharacter()
        {
            for (int i = 0; i < _charactersOutline.Length; i++)
                _charactersOutline[i].enabled = i == _selectedCharacter;
        }
    }
}
