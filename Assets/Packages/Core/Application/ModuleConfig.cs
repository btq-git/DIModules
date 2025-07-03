using BTQ.Core.Application;
using UnityEngine;
using VContainer;

namespace BTQ.Core.Module
{
	// Stores data relevant to a module
	public abstract class ModuleConfig : ScriptableObject
	{
		// Register relevant objects
		public abstract void Configure(IContainerBuilder builder);
		
		public IModule Module { get; protected set; }
	}

	public abstract class ModuleConfig<T> : ModuleConfig where T : IModule
	{
		public override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(this).AsSelf();
			builder.Register<T>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
		}
	}
}