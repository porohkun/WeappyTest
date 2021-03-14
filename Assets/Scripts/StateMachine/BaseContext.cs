using UnityEngine;

namespace WeappyTest
{
    public abstract class BaseContext
    {
        private SpriteRenderer _spriteRenderer;
        public Animator Animator { get; private set; }

        private Direction _direction;
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

        public BaseContext(SpriteRenderer spriteRenderer, Animator animator)
        {
            _spriteRenderer = spriteRenderer;
            Animator = animator;
        }
    }
}
