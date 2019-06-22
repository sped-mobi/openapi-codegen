// -----------------------------------------------------------------------
// <copyright file="ScaffoldingManager.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repository;
using Microsoft.OpenApi.CodeGeneration.RepositoryInterface;
using Microsoft.OpenApi.CodeGeneration.Supervisor;
using Microsoft.OpenApi.CodeGeneration.SupervisorInterface;
using Microsoft.OpenApi.CodeGeneration.ViewModels;

namespace Microsoft.OpenApi.CodeGeneration.Scaffolding
{
    public class ScaffoldingManager : IScaffoldingManager
    {
        private readonly IConfigurationScaffolder _configuration;
        private readonly IControllerScaffolder _controller;
        private readonly IConverterScaffolder _converter;
        private readonly IEntityScaffolder _entity;
        private readonly IRepositoryScaffolder _repository;
        private readonly IRepositoryInterfaceScaffolder _repositoryInterface;
        private readonly IViewModelScaffolder _viewModel;
        private readonly ISupervisorScaffolder _supervisor;
        private readonly ISupervisorInterfaceScaffolder _supervisorInterface;
        private readonly IContextScaffolder _context;

        public ScaffoldingManager(
            IConfigurationScaffolder configuration,
            IControllerScaffolder controller,
            IConverterScaffolder converter,
            IEntityScaffolder entity,
            IRepositoryScaffolder repository,
            IRepositoryInterfaceScaffolder repositoryInterface,
            IViewModelScaffolder viewModel,
            ISupervisorScaffolder supervisor,
            ISupervisorInterfaceScaffolder supervisorInterface,
            IContextScaffolder context)
        {
            _configuration = configuration;
            _controller = controller;
            _converter = converter;
            _entity = entity;
            _repository = repository;
            _repositoryInterface = repositoryInterface;
            _viewModel = viewModel;
            _supervisor = supervisor;
            _supervisorInterface = supervisorInterface;
            _context = context;
        }

        public void SaveModel(ScaffoldedModel model)
        {
            _configuration.Save(model.Configuration);
            _controller.Save(model.Controller);
            _converter.Save(model.Converter);
            _entity.Save(model.Entity);
            _repository.Save(model.Repository);
            _repositoryInterface.Save(model.RepoisitoryInterfaces);
            _viewModel.Save(model.ViewModel);
            _supervisor.Save(model.Supervisor);
            _supervisorInterface.Save(model.SupervisorInterface);
            _context.Save(model.Context);
        }

        public ScaffoldedModel ScaffoldModel(OpenApiOptions options)
        {
            return new ScaffoldedModel
            {
                Configuration = _configuration.ScaffoldModel(options),
                Controller = _controller.ScaffoldModel(options),
                Converter = _converter.ScaffoldModel(options),
                Entity = _entity.ScaffoldModel(options),
                Repository = _repository.ScaffoldModel(options),
                RepoisitoryInterfaces = _repositoryInterface.ScaffoldModel(options),
                ViewModel = _viewModel.ScaffoldModel(options),
                Supervisor = _supervisor.ScaffoldModel(options),
                SupervisorInterface = _supervisorInterface.ScaffoldModel(options),
                Context = _context.ScaffoldModel(options)
            };
        }
    }
}
