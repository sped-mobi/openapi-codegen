using System.IO;

namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class FileWriter
    {
        public static void WriteFile(GeneratedFile file)
        {
            EnsureDirectoryAndFile(file.Path);
            File.WriteAllText(file.Path, file.Code);
        }

        private static void EnsureDirectoryAndFile(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
