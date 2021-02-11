using System;
using System.Collections.Generic;
using IServiceProvider = PingPong.Core.Providers.Abstraction.IServiceProvider;

namespace PingPong.Core.Providers
{
	public class ServiceProvider : IServiceProvider
	{
		public static ServiceProvider Instance { get; } = new ServiceProvider();

		private readonly Dictionary<Type, Func<IServiceProvider, object>> _serviceFactories;
		private readonly Dictionary<Type, object> _serviceContainer;

		private ServiceProvider()
		{
			_serviceFactories = new Dictionary<Type, Func<IServiceProvider, object>>();
			_serviceContainer = new Dictionary<Type, object>();
		}

		public void Register<TService>(TService service) where TService : class
		{
			_serviceContainer.Add(typeof(TService), service);
		}

		public void Register<TService>() where TService : class, new()
		{
			Register(_ => new TService());
		}

		public void Register<TService>(Func<IServiceProvider, TService> factory) where TService : class
		{
			_serviceFactories.Add(typeof(TService), factory);
		}

		public void Register<TInterface, TService>(TService service) where TService : class, TInterface
		{
			_serviceContainer.Add(typeof(TInterface), service);
		}

		public void Register<TInterface, TService>() where TService : class, TInterface, new()
		{
			Register<TInterface, TService>(_ => new TService());
		}

		public void Register<TInterface, TService>(Func<IServiceProvider, TService> factory) where TService : class, TInterface
		{
			_serviceFactories.Add(typeof(TInterface), factory);
		}

		public TService Resolve<TService>() where TService : class
		{
			var serviceType = typeof(TService);

			if (_serviceContainer.ContainsKey(serviceType))
				return (TService) _serviceContainer[serviceType];

			if (!_serviceFactories.ContainsKey(serviceType))
				return null;

			var service = (TService)_serviceFactories[serviceType](this);
			_serviceContainer.Add(serviceType, service);

			return service;
		}
	}
}
