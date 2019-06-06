using Microsoft.OpenApi.CodeGeneration.Configurations;
using Microsoft.OpenApi.CodeGeneration.Context;
using Microsoft.OpenApi.CodeGeneration.Controllers;
using Microsoft.OpenApi.CodeGeneration.Converters;
using Microsoft.OpenApi.CodeGeneration.Entities;
using Microsoft.OpenApi.CodeGeneration.Repositories;
using Microsoft.OpenApi.CodeGeneration.Supervisor;
using Microsoft.OpenApi.CodeGeneration.ViewModels;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class ScaffoldedModel
    {

        public ConfigurationModel Configuration { get; set; }

        public ControllerModel Controller { get; set; }

        public ConverterModel Converter { get; set; }

        public EntityModel Entity { get; set; }

        public RepositoryModel Repository { get; set; }

        public RepositoryInterfaceModel RepoisitoryInterfaces { get; set; }

        public ViewModelModel ViewModel { get; set; }

        public ContextModel Context { get; set; }

        public SupervisorModel Supervisor { get; set; }
    }
}
