using System;

namespace PingPong.Core.Providers.Abstraction
{
	public interface IServiceProvider
	{
		void Register<TService>() where TService : class, new();
		void Register<TService>(TService service) where TService : class;
		void Register<TService>(Func<IServiceProvider, TService> factory) where TService : class;

		void Register<TInterface, TService>() where TService : class, TInterface, new();
		void Register<TInterface, TService>(TService service) where TService : class, TInterface;
		void Register<TInterface, TService>(Func<IServiceProvider, TService> factory) where TService : class, TInterface;

		TService Resolve<TService>() where TService : class;
	}
}
