namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class FileFactory
    {
        public static GeneratedFile Create(CodeGenerationOptions options, FileType type, Kind kind = Kind.None)
        {
            GeneratedFile file = new GeneratedFile();
            file.Path = FilePathHelper.GetFilePath(options.OutputDir, type, kind);
            file.Code = CodeGenerator.Generate(options, type, kind);
            return file;
        }
    }
}
