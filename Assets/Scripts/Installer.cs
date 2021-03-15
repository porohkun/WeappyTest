using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace WeappyTest
{
    public class Installer : MonoInstaller
    {
        [Inject]
        private Slime.Slime.Settings _slimeSettings;
        [Inject]
        private Character.Character.Settings _characterSettings;

        public override void InstallBindings()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types.Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(ZenjectBindingAsSingleAttribute))))
                Container.BindInterfacesAndSelfTo(type).AsSingle();

            foreach (var type in types.Where(t => !t.IsAbstract && typeof(BasePage).IsAssignableFrom(t)))
                Container.BindInterfacesAndSelfTo(typeof(NavigatePageCommand<>).MakeGenericType(type)).AsSingle();

            InstallExistingMonoBehaviours();

            Container.BindFactory<Character.CharacterContext, StateMachine<Character.CharacterContext>, StateMachine<Character.CharacterContext>.Factory>();
            Container.BindInterfacesAndSelfTo<StateFactory<Character.CharacterContext>>().AsSingle();

            Container.BindFactory<Ball.BallContext, StateMachine<Ball.BallContext>, StateMachine<Ball.BallContext>.Factory>();
            Container.BindInterfacesAndSelfTo<StateFactory<Ball.BallContext>>().AsSingle();

            Container.BindFactory<Boss.BossContext, StateMachine<Boss.BossContext>, StateMachine<Boss.BossContext>.Factory>();
            Container.BindInterfacesAndSelfTo<StateFactory<Boss.BossContext>>().AsSingle();

            Container.BindFactory<Slime.SlimeContext, StateMachine<Slime.SlimeContext>, StateMachine<Slime.SlimeContext>.Factory>();
            Container.BindInterfacesAndSelfTo<StateFactory<Slime.SlimeContext>>().AsSingle();

            Container.BindFactory<Slime.Slime, Slime.Slime.Factory>().FromComponentInNewPrefab(_slimeSettings.Prefab);

            Container.BindFactory<Character.Character, Character.Character.FactoryChip>().FromComponentInNewPrefab(_characterSettings.Prefabs[0]);
            Container.BindFactory<Character.Character, Character.Character.FactoryDale>().FromComponentInNewPrefab(_characterSettings.Prefabs[1]);
        }

        public override void Start()
        {
            base.Start();
            Container.Resolve<NavigatePageCommand<MenuPage>>().Execute();
        }

        private void InstallExistingMonoBehaviours()
        {
            Scene scene = SceneManager.GetActiveScene();
            foreach (var go in scene.GetRootGameObjects())
                InstallGameObject(go);
        }

        private void InstallGameObject(GameObject go)
        {
            Container.BindInstances(go.GetComponents<Component>()
                .Where(c => c.GetType().CustomAttributes.Any(a => a.AttributeType == typeof(ZenjectBindingInstanceAsSingleAttribute)))
                .ToArray());

            for (int i = 0; i < go.transform.childCount; i++)
                InstallGameObject(go.transform.GetChild(i).gameObject);
        }
    }
}
