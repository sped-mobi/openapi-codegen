using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration
{
    internal static class Replacer
    {
        public static Dictionary<string, string> Create(OpenApiOptions options)
        {
            return CreateReplacementsDictionary(
                options.SolutionName,
                options.Document.Info.Description,
                options.RootNamespace,
                options.RepositoryName,
                TargetFramework.NetCoreApp_2_2);
        }

        public static string Replace(string input, Dictionary<string, string> replacements)
        {
            string retVal = input;
            foreach (var kvp in replacements)
            {
                retVal = retVal.Replace(kvp.Key, kvp.Value);
            }

            return retVal;
        }

        private static Dictionary<string, string> CreateReplacementsDictionary(
            string name,
            string description,
            string defaultNamespace,
            string repositoryName,
            TargetFramework targetFramework)
        {
            string tf = string.Empty;
            switch (targetFramework)
            {
                case TargetFramework.Net_472:
                    tf = "net472";
                    break;
                case TargetFramework.NetStandard_2_0:
                    tf = "netstandard2.0";
                    break;
                case TargetFramework.NetCoreApp_2_0:
                    tf = "netcoreapp2.0";
                    break;
                case TargetFramework.NetCoreApp_2_1:
                    tf = "netcoreapp2.1";
                    break;
                case TargetFramework.NetCoreApp_2_2:
                    tf = "netcoreapp2.2";
                    break;
                case TargetFramework.NetCoreApp_3_0:
                    tf = "netcoreapp3.0";
                    break;
            }

            return new Dictionary<string, string>
            {
                ["{{name}}"] = name,
                ["{{repositoryName}}"] = repositoryName,
                ["{{targetFramework}}"] = tf,
                ["{{defaultNamespace}}"] = defaultNamespace,
                ["{{description}}"] = description

                //["{{codeGeneratorGuid}}"] = GenerateGuid(),
                //["{{buildGuid}}"] = GenerateGuid(),
                //["{{toolsGuid}}"] = GenerateGuid(),
                //["{{arcadeRepoGuid}}"] = GenerateGuid(),
                //["{{solutionGuid}}"] = GenerateGuid()
            };
        }

        private static string GenerateGuid() =>
            Guid.NewGuid().ToString("B").ToUpper();
    }
}