using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "SettingsInstaller", order = 51)]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public override void InstallBindings()
        {

        }
    }
}
