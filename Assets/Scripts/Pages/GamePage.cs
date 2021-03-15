using UnityEngine;

namespace WeappyTest
{
    [ZenjectBindingInstanceAsSingle]
    public class GamePage : BasePage
    {
        [SerializeField]
        private Character.Character _character;
        [SerializeField]
        private SpriteRenderer[] _livesSprites;

        private void Update()
        {
            for (int i = 0; i < _livesSprites.Length; i++)
                _livesSprites[i].enabled = i < _character.Context.Lives;
        }
    }
}
