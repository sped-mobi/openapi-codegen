using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.Supervisor;
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
        private readonly IViewModelScaffolder _viewModel;
        private readonly ISupervisorScaffolder _supervisor;
        private readonly IContextScaffolder _context;

        public ScaffoldingManager(
            IConfigurationScaffolder configuration,
            IControllerScaffolder controller,
            IConverterScaffolder converter,
            IEntityScaffolder entity,
            IRepositoryScaffolder repository,
            IViewModelScaffolder viewModel,
            ISupervisorScaffolder supervisor,
            IContextScaffolder context)
        {
            _configuration = configuration;
            _controller = controller;
            _converter = converter;
            _entity = entity;
            _repository = repository;
            _viewModel = viewModel;
            _supervisor = supervisor;
            _context = context;
        }

        public void SaveModel(ScaffoldedModel model)
        {
            _configuration.Save(model.Configuration);
            _controller.Save(model.Controller);
            _converter.Save(model.Converter);
            _entity.Save(model.Entity);
            _repository.Save(model.Repository);
            _viewModel.Save(model.ViewModel);
            _supervisor.Save(model.Supervisor);
            _context.Save(model.Context);
        }

        public ScaffoldedModel ScaffoldModel(OpenApiOptions options)
        {
            ScaffoldedModel model = new ScaffoldedModel();
            model.Configuration = _configuration.ScaffoldModel(options);
            model.Controller = _controller.ScaffoldModel(options);
            model.Converter = _converter.ScaffoldModel(options);
            model.Entity = _entity.ScaffoldModel(options);
            model.Repository = _repository.ScaffoldModel(options);
            model.ViewModel = _viewModel.ScaffoldModel(options);
            model.Supervisor = _supervisor.ScaffoldModel(options);
            model.Context = _context.ScaffoldModel(options);
            return model;
        }
    }
}
