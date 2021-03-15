using UnityEngine;

namespace WeappyTest
{
    public class SlimeSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Direction _direction;

        public Direction Direction => _direction;
    }
}
