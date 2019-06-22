// -----------------------------------------------------------------------
// <copyright file="FileWriter.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace Microsoft.OpenApi.CodeGeneration
{
    public class FileWriter : IFileWriter
    {
        public void WriteFile(ScaffoldedFile file)
        {
            EnsureDirectoryAndFile(file.Path);
            File.WriteAllText(file.Path, file.Code);
        }

        private static void EnsureDirectoryAndFile(string path)
        {
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void WriteFiles(IEnumerable<ScaffoldedFile> files)
        {
            foreach (var file in files)
            {
                WriteFile(file);
            }
        }
    }
}
