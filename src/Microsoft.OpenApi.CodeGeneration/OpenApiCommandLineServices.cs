// -----------------------------------------------------------------------
// <copyright file="OpenApiServices.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Models;
using Microsoft.OpenApi.CodeGeneration.Projects;
using Microsoft.OpenApi.CodeGeneration.Repository;
using Microsoft.OpenApi.CodeGeneration.RepositoryInterface;
using Microsoft.OpenApi.CodeGeneration.Scaffolding;
using Microsoft.OpenApi.CodeGeneration.Supervisor;
using Microsoft.OpenApi.CodeGeneration.SupervisorInterface;
using Microsoft.OpenApi.CodeGeneration.Utilities;
using Microsoft.OpenApi.CodeGeneration.ViewModels;
using Microsoft.OpenApi.Readers;

namespace Microsoft.OpenApi.CodeGeneration
{



    public class OpenApiCommandLineServices
    {
        private IServiceProvider _serviceProvider;

        private readonly string _documentPath;

        public OpenApiCommandLineServices(string documentPath)
        {
            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException("The file was not found --> " + documentPath, documentPath);
            }

            _documentPath = documentPath;
        }

        public TService GetService<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    _serviceProvider = new ServiceCollection()
                        .AddSingleton<GeneratorDependencies>()
                        .AddSingleton<ScaffolderDependencies>()
                        .AddSingleton<IOpenApiDocument, OpenApiDocumentV3>(_ =>
                        {
                            using (FileStream stream = new FileStream(_documentPath, FileMode.Open, FileAccess.Read))
                            {
                                var sr = new OpenApiStreamReader(new OpenApiReaderSettings
                                {
                                    ReferenceResolution = ReferenceResolutionSetting.ResolveLocalReferences
                                });
                                var doc = sr.Read(stream, out var diagnostic);

                                return new OpenApiDocumentV3(doc);
                            }

                        })
                        .AddSingleton<IEntityModelBuilder, EntityModelBuilder>()
                        .AddSingleton<ISolutionFactory, SolutionFactory>()
                        .AddSingleton<IApiProjectScaffolder, ApiProjectScaffolder>()
                        .AddSingleton<ICoreProjectScaffolder, CoreProjectScaffolder>()
                        .AddSingleton<IDataProjectScaffolder, DataProjectScaffolder>()
                        .AddSingleton<ISolutionScaffolder, SolutionScaffolder>()
                        .AddSingleton<IApiProjectGenerator, ApiProjectGenerator>()
                        .AddSingleton<ICoreProjectGenerator, CoreProjectGenerator>()
                        .AddSingleton<IDataProjectGenerator, DataProjectGenerator>()
                        .AddSingleton<ISolutionGenerator, SolutionGenerator>()
                        .AddSingleton<IPluralizer, Pluralizer>()
                        .AddSingleton<ISchemaConverter, SchemaConverter>()
                        .AddSingleton<INameHelper, NameHelper>()
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
                        .AddSingleton<IRepositoryInterfaceGenerator, RepositoryInterfaceGenerator>()
                        .AddSingleton<IViewModelGenerator, ViewModelGenerator>()
                        .AddSingleton<IContextGenerator, ContextGenerator>()
                        .AddSingleton<ISupervisorGenerator, SupervisorGenerator>()
                        .AddSingleton<ISupervisorInterfaceGenerator, SupervisorInterfaceGenerator>()
                        .AddSingleton<ISupervisorInterfaceScaffolder, SupervisorInterfaceScaffolder>()
                        .AddSingleton<IConfigurationScaffolder, ConfigurationScaffolder>()
                        .AddSingleton<IControllerScaffolder, ControllerScaffolder>()
                        .AddSingleton<IConverterScaffolder, ConverterScaffolder>()
                        .AddSingleton<IEntityScaffolder, EntityScaffolder>()
                        .AddSingleton<IRepositoryScaffolder, RepositoryScaffolder>()
                        .AddSingleton<IRepositoryInterfaceScaffolder, RepositoryInterfaceScaffolder>()
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
