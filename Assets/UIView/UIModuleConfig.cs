using System.Collections.Generic;
using BTQ.Core.Application;
using Reflex.Core;
using UnityEngine;

namespace UIView
{
	[CreateAssetMenu(menuName = "BTQ/Module/UI View")]
	public class UIModuleConfig : ModuleConfig<UIModule>
	{
		[SerializeField]
		private ViewTransition defaultTransition;
		
		public Canvas canvas;
		
		public List<View> views = new();
		
		public List<View> dynamicallyCreatedViews = new();

		public override void Register(ContainerBuilder builder)
		{
			base.Register(builder);
			
			builder.AddSingleton(defaultTransition, typeof(ViewTransition)); // Default Transition
		}
	}
}