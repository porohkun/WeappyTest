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
        protected EffectKeeper _effects = new EffectKeeper();
        public T Context { get; private set; }

        protected abstract T CreateContext();

        [Inject]
        void Inject(MyPhysicsService myPhysicsService, StateMachine<T>.Factory stateMachineFactory)
        {
            _myPhysicsService = myPhysicsService;
            Context = CreateContext();
            _states = stateMachineFactory.Create(Context);
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
            Context.VerticalSpeed += Context.VerticalAcceleration * Time.deltaTime;

            _states.Update();
            _effects.Update();

            transform.position += new Vector3(Context.HorizontalSpeed * (int)Context.Direction, 0f) * Time.deltaTime;
            CheckCollisionsHorizontal();
            transform.position += new Vector3(0f, Context.VerticalSpeed) * Time.deltaTime;
            CheckCollisionsVertical();

            OnUpdate();
        }

        protected abstract void CheckCollisionsHorizontal();
        protected abstract void CheckCollisionsVertical();
        protected virtual void OnUpdate() { }
    }
}
