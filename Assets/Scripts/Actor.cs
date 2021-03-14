using UnityEngine;
using Zenject;

namespace WeappyTest
{
    public abstract class Actor<T> : MonoBehaviour where T : BaseContext
    {
        [SerializeField]
        protected Animator _animator;
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        [SerializeField]
        protected MyBoxCollider2D _collider;

        protected MyPhysicsService _myPhysicsService;
        protected StateMachine<T> _states;
        protected T _context;

        protected abstract T CreateContext();

        [Inject]
        void Inject(MyPhysicsService myPhysicsService, StateMachine<T>.Factory stateMachineFactory)
        {
            _myPhysicsService = myPhysicsService;
            _context = CreateContext();
            _states = stateMachineFactory.Create(_context);
        }

        public void TriggerState<TState>() where TState : BaseState<T>
        {
            _states.ForceState<TState>();
        }

        private void Awake()
        {
            ConfigureBehaviour();
        }

        protected abstract void ConfigureBehaviour();

        private void Update()
        {
            _context.VerticalSpeed += _context.VerticalAcceleration * Time.deltaTime;

            _states.Update();

            transform.position += new Vector3(_context.HorizontalSpeed * (int)_context.Direction, 0f) * Time.deltaTime;
            CheckCollisionsHorizontal();
            transform.position += new Vector3(0f, _context.VerticalSpeed) * Time.deltaTime;
            CheckCollisionsVertical();

            OnUpdate();
        }

        protected abstract void CheckCollisionsHorizontal();
        protected abstract void CheckCollisionsVertical();
        protected virtual void OnUpdate() { }
    }
}
