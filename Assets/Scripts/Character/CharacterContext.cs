using UnityEngine;

namespace WeappyTest
{
    public class CharacterContext : IStateContext
    {
        private Character _character;
        private SpriteRenderer _spriteRenderer;
        private Direction _direction;

        public Animator Animator { get; private set; }
        public Direction Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                _spriteRenderer.flipX = Direction == Direction.Left;
            }
        }

        public float HorizontalSpeed { get; internal set; }
        public float VerticalSpeed { get; internal set; }
        public float VerticalAcceleration { get; internal set; }
        public bool TouchFloor { get; internal set; }

        public CharacterContext(Character character, SpriteRenderer spriteRenderer, Animator animator)
        {
            _character = character;
            _spriteRenderer = spriteRenderer;
            Animator = animator;
        }
    }
}
