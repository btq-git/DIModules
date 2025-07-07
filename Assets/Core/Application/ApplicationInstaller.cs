using Reflex.Core;
using UnityEngine;

namespace BTQ.Core.Application
{
    // MonoBehaviour necessary for Reflex DI
    public class ApplicationInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private ApplicationModulesList list;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            foreach (var moduleConfig in list.modules)
            {
                containerBuilder.AddSingleton(moduleConfig);
                moduleConfig.Register(containerBuilder);
            }
        }
    }
}