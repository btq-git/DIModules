using Reflex.Core;
using UnityEngine;

namespace BTQ.Core.Application
{
    // Stores data relevant to a module
    public abstract class ModuleConfig : ScriptableObject
    {
        public abstract void Register(ContainerBuilder builder);
    }

    public abstract class ModuleConfig<T> : ModuleConfig where T : ModuleBase
    {
        public override void Register(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(T));

            builder.OnContainerBuilt += OnContainerBuilt;
        }

        protected virtual void OnContainerBuilt(Container container)
        {
            container.Resolve<T>();
        }
    }
}