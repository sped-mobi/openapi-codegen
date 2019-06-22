using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.OpenApi.CodeGeneration
{
    internal static class RepositoryCreator
    {
        public static string CreateRepository(OpenApiOptions options)
        {
            string zipFilePath = Path.Combine(options.OutputDir, "arcade.zip");
            string repositoryDirectory = Path.Combine(options.OutputDir, options.SolutionName);
            string solutionFilePath = Path.Combine(repositoryDirectory, $"{options.SolutionName}.sln");
            SlowlyRemoveAndRecreateDirectory(repositoryDirectory);
            RepositoryDownloader.DownloadFile(DownloadUrls.Arcade, zipFilePath);
            RepositoryExpander.ExpandFile(zipFilePath, repositoryDirectory);
            Directory.Delete(
                Path.Combine(repositoryDirectory, "src", "ArcadeRepo"),
                true);
            File.Delete(
                Path.Combine(repositoryDirectory, "ArcadeRepo.sln"));
            var replacements = Replacer.Create(options);
            Process(repositoryDirectory, replacements);
            return Path.Combine(repositoryDirectory, "src");
        }

        private static void Process(string targetDirectory, Dictionary<string, string> replacements)
        {
            var files = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories).Select(x => new FileInfo(x));
            foreach (var file in files)
            {
                ProcessFileInfo(file, replacements);
            }
        }

        private static void ProcessFileInfo(FileInfo info, Dictionary<string, string> replacements)
        {
            string text = Replacer.Replace(File.ReadAllText(info.FullName), replacements);
            Console.WriteLine($"Processing file {info.Name}");
            File.WriteAllText(info.FullName, text);
        }

        private static void SlowlyRemoveAndRecreateDirectory(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                string[] files = Directory.GetFiles(targetDirectory, "*.*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                try
                {
                    Directory.Delete(targetDirectory, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                Directory.CreateDirectory(targetDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}