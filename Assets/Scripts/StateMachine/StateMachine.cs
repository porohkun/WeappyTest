using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace WeappyTest
{
    public class StateMachine<TContext> where TContext : IStateContext
    {
        private readonly StateFactory<TContext> _stateFactory;
        private readonly TContext _context;
        private Tuple<IState<TContext>, List<Transition<TContext>>> _currentState;
        private readonly Dictionary<Type, Tuple<IState<TContext>, List<Transition<TContext>>>> _states = new Dictionary<Type, Tuple<IState<TContext>, List<Transition<TContext>>>>();

        public StateMachine(StateFactory<TContext> stateFactory, TContext context)
        {
            _stateFactory = stateFactory;
            _context = context;
        }

        public void AddState<TState>() where TState : IState<TContext>
        {
            _states[typeof(TState)] = new Tuple<IState<TContext>, List<Transition<TContext>>>(_stateFactory.Create<TState>(), new List<Transition<TContext>>());
            if (_currentState == null)
                ChangeCurrentState(_states.Keys.First());
        }

        private void ChangeCurrentState(Type newState)
        {
            if (_currentState != null)
                _currentState.Item1.OnExit(_context);
            _currentState = _states[newState];
            _currentState.Item1.OnEnter(_context);
        }

        public void AddTransition<TStateFrom, TStateTo>(Func<TContext, bool> condition)
            where TStateFrom : IState<TContext>
            where TStateTo : IState<TContext>
        {
            _states[typeof(TStateFrom)].Item2.Add(Transition<TContext>.New<TStateTo>(condition));
        }

        public void Update()
        {
            foreach (var transition in _currentState.Item2)
                if (transition.Condition(_context))
                {
                    ChangeCurrentState(transition.TargetState);
                    break;
                }
            _currentState.Item1.Update(_context);
        }

        private class Transition<Tcontext> where Tcontext : IStateContext
        {
            public readonly Type TargetState;
            public readonly Func<TContext, bool> Condition;

            private Transition(Func<TContext, bool> condition, Type targetState)
            {
                TargetState = targetState;
                Condition = condition;
            }

            public static Transition<Tcontext> New<TTargetState>(Func<TContext, bool> condition)
            {
                return new Transition<Tcontext>(condition, typeof(TTargetState));
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
