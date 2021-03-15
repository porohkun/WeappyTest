using Zenject;

namespace WeappyTest
{
    public interface IEffect
    {
    }

    [ZenjectBindingAsSingle]
    public class EffectFactory
    {
        readonly DiContainer _container;

        public EffectFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : IEffect
        {
            return _container.Instantiate<T>();
        }
    }
}
