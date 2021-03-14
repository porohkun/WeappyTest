using Zenject;

namespace WeappyTest
{
    public interface IState<TContext> where TContext : IStateContext
    {
        void OnEnter(TContext context);
        void Update(TContext context);
        void OnExit(TContext context);
    }

    [ZenjectBindingAsSingle]
    public class StateFactory<TStateContext> where TStateContext : IStateContext
    {
        readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : IState<TStateContext>
        {
            return _container.Instantiate<T>();
        }
    }
}
