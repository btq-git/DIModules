using System.Collections.Generic;
using BTQ.Core.Module;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BTQ.Core
{
	public class ApplicationLifetimeScope : LifetimeScope
	{
		[Header("Services")]
		[SerializeField]
		private List<ModuleConfig> moduleConfigs;

		protected override void Configure(IContainerBuilder builder)
		{
			foreach (var config in moduleConfigs)
			{
				config.Configure(builder);
			}
		}
	}
}