﻿using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Owin;
using ShortLink.Application.Services;
using ShortLink.DataAccess;
using ShortLink.DataAccess.Repositories;
using ShortLink.Modules;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace ShortLink
{
    public static class Bootstrapper
    {
        public static void Initialize(IAppBuilder app)
        {
            ConfigureUniqueIdentifierGenerator();
            DIConfiguration(app);
        }

        private static void DIConfiguration(IAppBuilder app)
        {
            var mvcContainer = new Container();
            var apiContainer = new Container();

            mvcContainer.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            apiContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            DataAccessModule.Load(mvcContainer);
            DataAccessModule.Load(apiContainer);

            ApplicationModule.Load(mvcContainer);
            ApplicationModule.Load(apiContainer);

            mvcContainer.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            apiContainer.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            mvcContainer.Verify();
            apiContainer.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(mvcContainer));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(apiContainer);
        }

        private static void ConfigureUniqueIdentifierGenerator()
        {
            string firstId = String.Empty;
            using (var linkRepository = new LinkRepository(new LinkDataContext()))
            {
                var link = linkRepository.LastOrDefault();
                if (link != null)
                {
                    firstId = link.Id;
                }
            }

            UniqueIdGenerator.Configure(firstId);
        }
    }
}