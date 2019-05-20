namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public enum FileType
    {
        ServicesClass,

        ContextInterface,
        ContextClass,
        ContextModelClass,

        SupervisorInterface,
        SupervisorClass,
        SupervisorModelClass,

        ScaffoldedModelClass,

        ModelClass,

        GeneratorClass,
        ScaffolderClass,

        GeneratorInterface,
        ScaffolderInterface,
    }
}
