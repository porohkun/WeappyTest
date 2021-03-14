using System;
using UnityEngine;
using Zenject;

namespace WeappyTest
{
    public class MyBoxCollider2D : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D _collider;

        public event Action<MyBoxCollider2D> Destroyed;
        public Rect Bounds => new Rect(transform.position.ToVector2() + _collider.offset - _collider.size / 2, _collider.size);

        [Inject]
        void Inject(MyPhysicsService myPhysicsService)
        {
            myPhysicsService.Register(this);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        //private void Update()
        //{

        //}
    }
}
