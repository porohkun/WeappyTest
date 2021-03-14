using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "SettingsInstaller", order = 51)]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField]
        private Character.Settings _characterSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_characterSettings);
        }
    }
}
