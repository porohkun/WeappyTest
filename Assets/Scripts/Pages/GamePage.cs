using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [ZenjectBindingInstanceAsSingle]
    public class GamePage : BasePage
    {
        [SerializeField]
        private SpriteRenderer[] _livesSprites;

        private Character.Character _character;

        private PlaceholderFactory<Character.Character>[] _factories;
        [Inject]
        private void Inject(Character.Character.FactoryChip chipFactory, Character.Character.FactoryDale daleFactory)
        {
            _factories = new PlaceholderFactory<Character.Character>[] { chipFactory, daleFactory };
        }

        private void Awake()
        {
            _character = _factories[MenuPage.SelectedCharacter].Create();
            _character.transform.SetParent(transform);
            _character.transform.position = new Vector3(-96, -74);
        }

        private void Update()
        {
            for (int i = 0; i < _livesSprites.Length; i++)
                _livesSprites[i].enabled = i < _character.Context.Lives;
        }
    }
}
