namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class NameHelper
    {
        public static string MakeName(Kind kind)
        {
            return kind.ToString();
        }

        public static string MakeModelClassName(Kind kind)
        {
            switch (kind)
            {
                case Kind.Supervisor:
                    return "SupervisorModel";
                case Kind.Context:
                    return "ContextModel";
                case Kind.Configuration:
                    return "ConfigurationModel";
                case Kind.Controller:
                    return "ControllerModel";
                case Kind.Converter:
                    return "ConverterModel";
                case Kind.Entity:
                    return "EntityModel";
                case Kind.Repository:
                    return "RepositoryModel";
                case Kind.ViewModel:
                    return "ViewModelModel";
                default:
                    return string.Empty;
            }
        }

        public static string MakeGeneratorClassName(Kind kind)
        {
            return MakeGeneratorInterfaceName(kind).Substring(1);
        }

        public static string MakeGeneratorInterfaceName(Kind kind)
        {
            switch (kind)
            {
                case Kind.Supervisor:
                    return "ISupervisorGenerator";
                case Kind.Context:
                    return "IContextGenerator";
                case Kind.Configuration:
                    return "IConfigurationGenerator";
                case Kind.Controller:
                    return "IControllerGenerator";
                case Kind.Converter:
                    return "IConverterGenerator";
                case Kind.Entity:
                    return "IEntityGenerator";
                case Kind.Repository:
                    return "IRepositoryGenerator";
                case Kind.ViewModel:
                    return "IViewModelGenerator";
                default:
                    return string.Empty;
            }
        }

        internal static string MakeServicesClassName()
        {
            return "OpenApiServices";
        }

        public static string MakeScaffolderClassName(Kind kind)
        {
            return MakeScaffolderInterfaceName(kind).Substring(1);
        }

        public static string MakeScaffolderInterfaceName(Kind kind)
        {
            switch (kind)
            {
                case Kind.Supervisor:
                    return "ISupervisorScaffolder";
                case Kind.Context:
                    return "IContextScaffolder";
                case Kind.Configuration:
                    return "IConfigurationScaffolder";
                case Kind.Controller:
                    return "IControllerScaffolder";
                case Kind.Converter:
                    return "IConverterScaffolder";
                case Kind.Entity:
                    return "IEntityScaffolder";
                case Kind.Repository:
                    return "IRepositoryScaffolder";
                case Kind.ViewModel:
                    return "IViewModelScaffolder";
                default:
                    return string.Empty;
            }
        }

        public static string MakeSupervisorClassName()
        {
            return "OpenApiSupervisor";
        }

        public static string MakeSupervisorInterfaceName()
        {
            return "I" + MakeSupervisorClassName();
        }

        public static string MakeContextClassName()
        {
            return "OpenApiContext";
        }

        public static string MakeContextInterfaceName()
        {
            return "I" + MakeContextClassName();
        }

        public static string MakeScaffoldedModelClassName()
        {
            return "ScaffoldedModel";
        }

        public static string MakeSupervisorModelClassName()
        {
            return "SupervisorModel";
        }

        public static string MakeContextModelClassName()
        {
            return "ContextModel";
        }
    }

    public static class NamespaceHelper
    {
        public static string MakeNamespace(string rootNamespace, Kind kind)
        {
            if (kind == Kind.None)
            {
                return rootNamespace;
            }

            return $"{rootNamespace}.{PluralNameHelper.MakePlural(kind)}";
        }
    }
}
