namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionModel
    {
        public ScaffoldedFile SolutionFile { get; set; }

        public ApiProjectModel ApiProject { get; set; }

        public CoreProjectModel CoreProject { get; set; }

        public DataProjectModel DataProject { get; set; }
    }
}