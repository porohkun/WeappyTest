﻿using UnityEngine;
using Zenject;

namespace WeappyTest
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "SettingsInstaller", order = 51)]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField]
        private Character.Character.Settings _characterSettings;
        [SerializeField]
        private Ball.Ball.Settings _ballSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_characterSettings);
            Container.BindInstance(_ballSettings);
        }
    }
}
