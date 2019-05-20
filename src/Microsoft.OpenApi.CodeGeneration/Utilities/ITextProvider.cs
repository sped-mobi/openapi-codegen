using System.Text;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public interface ITextProvider
    {
        StringBuilder Builder { get; }
    }
}
