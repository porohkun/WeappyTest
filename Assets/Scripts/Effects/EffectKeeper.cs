using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeappyTest
{
    public class EffectKeeper
    {
        private Dictionary<Type, float> _effects = new Dictionary<Type, float>();

        public EffectKeeper()
        {
        }

        public void Add<T>(float duration) where T : IEffect
        {
            _effects[typeof(T)] = Time.realtimeSinceStartup + duration;
        }

        public void Update()
        {
            var expired = _effects.Where(e => e.Value < Time.realtimeSinceStartup).Select(e => e.Key).ToArray();
            foreach (var effect in expired)
                _effects.Remove(effect);
        }

        public bool Active<T>() where T : IEffect
        {
            return _effects.ContainsKey(typeof(T));
        }
    }
}
