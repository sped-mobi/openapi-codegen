namespace Microsoft.OpenApi.CodeGeneration
{
    public abstract class AbstractScaffolder
    {
        protected AbstractScaffolder(ScaffolderDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        protected ScaffolderDependencies Dependencies { get; }
    }
}
