using System.IO;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class FilePathHelper
    {
        public static string GetFilePath(string outputDir, FileType type, Kind kind = Kind.None)
        {
            if (type.IsSingle() && kind == Kind.None)
            {
                switch (type)
                {
                    case FileType.ContextModelClass:
                    case FileType.ContextInterface:
                    case FileType.ContextClass:
                        return Path.Combine(outputDir, "Context", MakeFileName(type));
                    case FileType.SupervisorModelClass:
                    case FileType.SupervisorInterface:
                    case FileType.SupervisorClass:
                        return Path.Combine(outputDir, "Supervisor", MakeFileName(type));
                    case FileType.ServicesClass:
                    case FileType.ScaffoldedModelClass:
                        return Path.Combine(outputDir, MakeFileName(type));
                    default:
                        break;
                }
            }
            return Path.Combine(outputDir, PluralNameHelper.MakePlural(kind), MakeFileName(type, kind));
        }

        private static string MakeFileName(FileType type)
        {
            switch (type)
            {
                case FileType.ServicesClass:
                    return NameHelper.MakeServicesClassName() + ".cs";
                case FileType.SupervisorModelClass:
                    return NameHelper.MakeSupervisorModelClassName() + ".cs";
                case FileType.ContextModelClass:
                    return NameHelper.MakeContextModelClassName() + ".cs";
                case FileType.ScaffoldedModelClass:
                    return NameHelper.MakeScaffoldedModelClassName() + ".cs";
                case FileType.ContextClass:
                    return NameHelper.MakeContextClassName() + ".cs";
                case FileType.ContextInterface:
                    return NameHelper.MakeContextInterfaceName() + ".cs";
                case FileType.SupervisorClass:
                    return NameHelper.MakeSupervisorClassName() + ".cs";
                case FileType.SupervisorInterface:
                    return NameHelper.MakeSupervisorInterfaceName() + ".cs";
                default:
                    return string.Empty + ".cs";
            }
        }

        private static string MakeFileName(FileType type, Kind kind)
        {
            switch (type)
            {
                case FileType.ModelClass:
                    return NameHelper.MakeModelClassName(kind) + ".cs";
                case FileType.GeneratorClass:
                    return NameHelper.MakeGeneratorClassName(kind) + ".cs";
                case FileType.ScaffolderClass:
                    return NameHelper.MakeScaffolderClassName(kind) + ".cs";
                case FileType.GeneratorInterface:
                    return NameHelper.MakeGeneratorInterfaceName(kind) + ".cs";
                case FileType.ScaffolderInterface:
                    return NameHelper.MakeScaffolderInterfaceName(kind) + ".cs";
                default:
                    return string.Empty + ".cs";
            }
        }

        private static bool IsSingle(this FileType type)
        {
            switch (type)
            {
                case FileType.ContextInterface:
                case FileType.ContextClass:
                case FileType.ContextModelClass:
                case FileType.SupervisorInterface:
                case FileType.SupervisorClass:
                case FileType.SupervisorModelClass:
                case FileType.ScaffoldedModelClass:
                case FileType.ServicesClass:
                    return true;
                default:
                    return false;
            }
        }
    }
}
