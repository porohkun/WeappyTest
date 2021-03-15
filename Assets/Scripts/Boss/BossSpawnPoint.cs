using UnityEngine;

namespace WeappyTest
{
    public class BossSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private bool _lowFlight;

        public bool LowFlight => _lowFlight;
        public Vector3 Position => transform.position;
    }
}
