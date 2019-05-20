using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Supervisor;

namespace Microsoft.OpenApi.CodeGeneration
{
    public static class OpenApiServices
    {
        private static IServiceProvider _serviceProvider;

        public static TService GetService<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }

        private static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    _serviceProvider = new ServiceCollection()
                        .AddSingleton<GeneratorDependencies>()
                        .AddSingleton<ScaffolderDependencies>()
                        .AddSingleton<IFileWriter, FileWriter>()
                        .AddSingleton<IPathHelper, PathHelper>()
                        .AddSingleton<ITextProvider, TextProvider>()
                        .AddSingleton<INamespaceHelper, NamespaceHelper>()
                        .AddSingleton<IScaffoldingManager, ScaffoldingManager>()
                        .AddSingleton<IConfigurationGenerator, ConfigurationGenerator>()
                        .AddSingleton<IControllerGenerator, ControllerGenerator>()
                        .AddSingleton<IConverterGenerator, ConverterGenerator>()
                        .AddSingleton<IEntityGenerator, EntityGenerator>()
                        .AddSingleton<IRepositoryGenerator, RepositoryGenerator>()
                        .AddSingleton<IViewModelGenerator, ViewModelGenerator>()
                        .AddSingleton<IContextGenerator, ContextGenerator>()
                        .AddSingleton<ISupervisorGenerator, SupervisorGenerator>()
                        .AddSingleton<IConfigurationScaffolder, ConfigurationScaffolder>()
                        .AddSingleton<IControllerScaffolder, ControllerScaffolder>()
                        .AddSingleton<IConverterScaffolder, ConverterScaffolder>()
                        .AddSingleton<IEntityScaffolder, EntityScaffolder>()
                        .AddSingleton<IRepositoryScaffolder, RepositoryScaffolder>()
                        .AddSingleton<IViewModelScaffolder, ViewModelScaffolder>()
                        .AddSingleton<IContextScaffolder, ContextScaffolder>()
                        .AddSingleton<ISupervisorScaffolder, SupervisorScaffolder>()
                        .BuildServiceProvider();
                }
                return _serviceProvider;
            }
        }
    }
}
