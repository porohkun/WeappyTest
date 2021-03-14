using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeappyTest
{
    [ZenjectBindingAsSingle]
    public class MyPhysicsService
    {
        private readonly List<MyBoxCollider2D> _colliders = new List<MyBoxCollider2D>();

        public void Register(MyBoxCollider2D collider)
        {
            _colliders.Add(collider);
            collider.Destroyed += Collider_Destroyed;
        }

        private void Collider_Destroyed(MyBoxCollider2D collider)
        {
            collider.Destroyed -= Collider_Destroyed;
            _colliders.Remove(collider);
        }

        public MyBoxCollider2D[] GetIntersections(MyBoxCollider2D collider)
        {
            return _colliders.Except(collider).Where(c => c.enabled && c.Bounds.Intersects(collider.Bounds)).ToArray();
        }
    }
}
