using System.Text;

namespace Microsoft.OpenApi.CodeGeneration.Utilities
{
    public class TextProvider : ITextProvider
    {
        private StringBuilder _builder;

        public StringBuilder Builder
        {
            get
            {
                if (_builder == null)
                {
                    _builder = new StringBuilder();
                }
                return _builder;
            }
        }
    }
}
