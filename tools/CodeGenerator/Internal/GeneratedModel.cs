using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public class GeneratedModel
    {
        public GeneratedFile ScaffoldedModelClass { get; set; }

        public GeneratedFile ServicesClassFile { get; set; }

        //public GeneratedFile ContextClass { get; set; }
        //public GeneratedFile ContextInterface { get; set; }
        //public GeneratedFile ContextModelClass { get; set; }

        //public GeneratedFile SupervisorClass { get; set; }
        //public GeneratedFile SupervisorInterface { get; set; }
        //public GeneratedFile SupervisorModelClass { get; set; }

        public IList<GeneratedFile> ScaffolderInterfaces { get; } = new List<GeneratedFile>();
        public IList<GeneratedFile> ScaffolderClasses { get; } = new List<GeneratedFile>();
        public IList<GeneratedFile> GeneratorInterfaces { get; } = new List<GeneratedFile>();
        public IList<GeneratedFile> GeneratorClasses { get; } = new List<GeneratedFile>();
        public IList<GeneratedFile> Models { get; } = new List<GeneratedFile>();
    }
}
