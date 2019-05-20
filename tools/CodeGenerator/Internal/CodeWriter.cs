using System;
using System.Text;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public class CodeWriter
    {
        private readonly ClipboardWriter _writer;
        private readonly StringBuilder _builder;

        public CodeWriter()
        {
            _builder = new StringBuilder();
            _writer = ClipboardWriter.CreateForStringBuilder(_builder);
        }

        public void PushIndent()
        {
            _writer.PushIndent();
        }

        public void PopIndent()
        {
            _writer.PopIndent();
        }

        public void Write(string value)
        {
            _writer.Write(value);
        }

        public void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        public void WriteLine()
        {
            _writer.WriteLine();
        }

        public IDisposable OpenBlock()
        {
            return _writer.OpenBlock();
        }

        public void Clear()
        {
            _builder.Clear();
        }

        public string GetText()
        {
            return _builder.ToString();
        }
    }
}
