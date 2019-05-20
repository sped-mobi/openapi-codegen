using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IFileWriter
    {
        void WriteFile(ScaffoldedFile file);

        void WriteFiles(IEnumerable<ScaffoldedFile> files);
    }
}
