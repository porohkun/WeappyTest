using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace WeappyTest
{
    public class StateMachine<TContext> where TContext : BaseContext
    {
        private readonly StateFactory<TContext> _stateFactory;
        private readonly TContext _context;
        private Tuple<BaseState<TContext>, List<Transition<TContext>>> _currentState;
        private readonly Dictionary<Type, Tuple<BaseState<TContext>, List<Transition<TContext>>>> _states = new Dictionary<Type, Tuple<BaseState<TContext>, List<Transition<TContext>>>>();

        public StateMachine(StateFactory<TContext> stateFactory, TContext context)
        {
            _stateFactory = stateFactory;
            _context = context;
        }

        public void AddState<TState>() where TState : BaseState<TContext>
        {
            _states[typeof(TState)] = new Tuple<BaseState<TContext>, List<Transition<TContext>>>(_stateFactory.Create<TState>(), new List<Transition<TContext>>());
            if (_currentState == null)
                ChangeCurrentState(_states.Keys.First());
        }

        public void ForceState<TState>() where TState : BaseState<TContext>
        {
            ChangeCurrentState(typeof(TState));
        }

        private void ChangeCurrentState(Type newState)
        {
            if (_currentState != null)
                _currentState.Item1.Exit(_context);
            _currentState = _states[newState];
            _currentState.Item1.Enter(_context);
            foreach (var transition in _currentState.Item2)
                transition.StartTime = Time.realtimeSinceStartup;
        }

        public void AddTransition<TStateFrom, TStateTo>(Func<TContext, bool> condition)
            where TStateFrom : BaseState<TContext>
            where TStateTo : BaseState<TContext>
        {
            _states[typeof(TStateFrom)].Item2.Add(Transition<TContext>.New<TStateTo>(condition));
        }

        public void AddTransition<TStateFrom, TStateTo>(float duration)
            where TStateFrom : BaseState<TContext>
            where TStateTo : BaseState<TContext>
        {
            _states[typeof(TStateFrom)].Item2.Add(Transition<TContext>.New<TStateTo>(duration));
        }

        public void Update()
        {
            bool stateChanged;
            do
            {
                stateChanged = false;
                foreach (var transition in _currentState.Item2)
                    if (transition.Condition(_context))
                    {
                        ChangeCurrentState(transition.TargetState);
                        Debug.Log($"State changed to {transition.TargetState}");
                        stateChanged = true;
                        break;
                    }
            }
            while (stateChanged);
            _currentState.Item1.Update(_context);
        }

        private class Transition<Tcontext> where Tcontext : BaseContext
        {
            public readonly Type TargetState;
            public readonly Func<TContext, bool> Condition;
            public readonly float Duration;

            public float StartTime { get; set; }

            private Transition(Func<TContext, bool> condition, Type targetState)
            {
                TargetState = targetState;
                Condition = condition;
            }

            private Transition(float duration, Type targetState)
            {
                TargetState = targetState;
                Duration = duration;
                Condition = c => Duration < (Time.realtimeSinceStartup - StartTime);
            }

            public static Transition<Tcontext> New<TTargetState>(Func<TContext, bool> condition)
            {
                return new Transition<Tcontext>(condition, typeof(TTargetState));
            }

            public static Transition<Tcontext> New<TTargetState>(float duration)
            {
                return new Transition<Tcontext>(duration, typeof(TTargetState));
            }
        }

        public class Factory : PlaceholderFactory<TContext, StateMachine<TContext>>
        {
            public override StateMachine<TContext> Create(TContext param)
            {
                return base.Create(param);
            }
        }
    }
}
