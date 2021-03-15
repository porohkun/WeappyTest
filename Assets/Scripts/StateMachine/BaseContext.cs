using UnityEngine;

namespace WeappyTest
{
    public abstract class BaseContext
    {
        private SpriteRenderer _spriteRenderer;
        public Animator Animator { get; private set; }
        public EffectKeeper Effects { get; private set; }

        private Direction _direction = Direction.Right;
        public virtual Direction Direction
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

        public BaseContext(EffectKeeper effects, SpriteRenderer spriteRenderer, Animator animator)
        {
            Effects = effects;
            _spriteRenderer = spriteRenderer;
            Animator = animator;
        }
    }
}
