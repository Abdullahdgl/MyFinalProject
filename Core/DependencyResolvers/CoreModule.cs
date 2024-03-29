﻿using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
	public class CoreModule : ICoreModule
	{
		public void load(IServiceCollection serviceCollection)
		{
			serviceCollection.AddMemoryCache();
			serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
			services.AddSingleton<Stopwatch>();

		}
	}
}
