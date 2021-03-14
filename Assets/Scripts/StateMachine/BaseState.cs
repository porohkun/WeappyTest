using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace WeappyTest
{
    public abstract class BaseState<TContext> where TContext : BaseContext
    {
        private float _startTime;
        private List<(float, Action)> _delays = new List<(float, Action)>();

        public void Enter(TContext context)
        {
            _startTime = Time.realtimeSinceStartup;
            OnEnter(context);
        }

        protected virtual void OnEnter(TContext context) { }

        public void Exit(TContext context)
        {
            _delays.Clear();
            OnExit(context);
        }

        protected virtual void OnExit(TContext context) { }

        public void Update(TContext context)
        {
            var invoked = new List<int>();
            for (int i = 0; i < _delays.Count; i++)
            {
                var delay = _delays[i];
                if (delay.Item1 <= Time.realtimeSinceStartup)
                {
                    delay.Item2?.Invoke();
                    invoked.Add(i);
                }
            }
            for (int i = invoked.Count - 1; i >= 0; i--)
                _delays.RemoveAt(invoked[i]);
            OnUpdate(context);
        }

        protected virtual void OnUpdate(TContext context) { }

        protected void SetDelay(float delay, Action delayAction)
        {
            if (delayAction != null && delay >= 0f)
                _delays.Add((Time.realtimeSinceStartup + delay, delayAction));
        }
    }

    [ZenjectBindingAsSingle]
    public class StateFactory<TStateContext> where TStateContext : BaseContext
    {
        readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : BaseState<TStateContext>
        {
            return _container.Instantiate<T>();
        }
    }
}
