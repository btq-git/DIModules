using System;

namespace BTQ.Core.Application
{
	public interface IModule : IDisposable
	{
		// Use IInitializable for quick self-initialisation
		// Use IAsyncStartable for asynchronous long setup
	}
}