// -----------------------------------------------------------------------
// <copyright file="RepositoryExpander.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Microsoft.OpenApi.CodeGeneration
{
    internal static class RepositoryExpander
    {
        public static void ExpandFile(string zipFile, string outputDirectory)
        {
            RepositoryDownloader.DownloadFile(DownloadUrls.Arcade, zipFile);
            string tempZipDir =
                Path.Combine(Path.GetDirectoryName(zipFile), Path.GetFileNameWithoutExtension(zipFile));
            ZipFile.ExtractToDirectory(zipFile, tempZipDir);
            var firstDirectory = Directory.GetDirectories(tempZipDir).SingleOrDefault();
            if (firstDirectory != null)
            {
                CopyDirectory(firstDirectory, outputDirectory, true, true);
            }

            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            if (Directory.Exists(tempZipDir))
            {
                Directory.Delete(tempZipDir, true);
            }
        }

        private static void CopyDirectory(string source, string destination, bool overwrite = false, bool deleteSourceOnCompletion = false)
        {
            DirectoryInfo dir = new DirectoryInfo(source);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(source);
            }

            var dirs = dir.GetDirectories();
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string targetPath = Path.Combine(destination, file.Name);
                file.CopyTo(targetPath, overwrite);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destination, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, overwrite, deleteSourceOnCompletion);
            }
        }
    }
}
