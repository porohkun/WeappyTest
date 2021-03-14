using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace WeappyTest
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in types.Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(ZenjectBindingAsSingleAttribute))))
                Container.BindInterfacesAndSelfTo(type).AsSingle();

            foreach (var type in types.Where(t => !t.IsAbstract && typeof(BasePage).IsAssignableFrom(t)))
                Container.BindInterfacesAndSelfTo(typeof(NavigatePageCommand<>).MakeGenericType(type)).AsSingle();

            InstallExistingMonoBehaviours();

            Container.BindFactory<CharacterContext, StateMachine<CharacterContext>, StateMachine<CharacterContext>.Factory>();
            Container.BindInterfacesAndSelfTo<StateFactory<CharacterContext>>().AsSingle();

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
