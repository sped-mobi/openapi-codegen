// -----------------------------------------------------------------------
// <copyright file="SolutionProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class SolutionGenerator : ProjectGeneratorBase, ISolutionGenerator
    {
        public SolutionGenerator(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public override string WriteProjectFile(OpenApiOptions options)
        {
            Clear();
            string coreProjectName = options.CoreProjectName;
            string dataProjectName = options.DataProjectName;
            string apiProjectName = options.ApiProjectName;
            string coreProjectGuid = GenerateGuid();
            string dataProjectGuid = GenerateGuid();
            string apiProjectGuid = GenerateGuid();
            string buildProjectGuid = GenerateGuid();
            WriteLine("Microsoft Visual Studio Solution File, Format Version 12.00");
            WriteLine("# Visual Studio Version 16");
            WriteLine("VisualStudioVersion = 16.0.29001.49");
            WriteLine("MinimumVisualStudioVersion = 10.0.40219.1");
            WriteLine($"Project(\"{{2150E333-8FDC-42A3-9474-1A3956D46DE8}}\") = \"Build\", \"Build\", \"{buildProjectGuid}\"");
            WriteLine("	ProjectSection(SolutionItems) = preProject");
            WriteLine("		.editorconfig = .editorconfig");
            WriteLine("		.gitignore = .gitignore");
            WriteLine("		Directory.Build.props = Directory.Build.props");
            WriteLine("		Directory.Build.targets = Directory.Build.targets");
            WriteLine("		eng\\VisualStudio.props = eng\\VisualStudio.props");
            WriteLine("		eng\\VisualStudio.targets = eng\\VisualStudio.targets");
            WriteLine("		global.json = global.json");
            WriteLine("		NuGet.config = NuGet.config");
            WriteLine("		README.md = README.md");
            WriteLine("		eng\\Versions.props = eng\\Versions.props");
            WriteLine("	EndProjectSection");
            WriteLine("EndProject");
            WriteLine(
                $"Project(\"{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}\") = \"{coreProjectName}\", \"src\\{coreProjectName}\\{coreProjectName}.csproj\", \"{coreProjectGuid}\"");
            WriteLine("EndProject");
            WriteLine(
                $"Project(\"{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}\") = \"{dataProjectName}\", \"src\\{dataProjectName}\\{dataProjectName}.csproj\", \"{dataProjectGuid}\"");
            WriteLine("EndProject");
            WriteLine(
                $"Project(\"{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}\") = \"{apiProjectName}\", \"src\\{apiProjectName}\\{apiProjectName}.csproj\", \"{apiProjectGuid}\"");
            WriteLine("EndProject");
            WriteLine("Global");
            WriteLine("	GlobalSection(SolutionConfigurationPlatforms) = preSolution");
            WriteLine("		Debug|Any CPU = Debug|Any CPU");
            WriteLine("		Release|Any CPU = Release|Any CPU");
            WriteLine("	EndGlobalSection");
            WriteLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution");
            WriteLine($"		{coreProjectGuid}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            WriteLine($"		{coreProjectGuid}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            WriteLine($"		{coreProjectGuid}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            WriteLine($"		{coreProjectGuid}.Release|Any CPU.Build.0 = Release|Any CPU");
            WriteLine($"		{dataProjectGuid}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            WriteLine($"		{dataProjectGuid}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            WriteLine($"		{dataProjectGuid}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            WriteLine($"		{dataProjectGuid}.Release|Any CPU.Build.0 = Release|Any CPU");
            WriteLine($"		{apiProjectGuid}.Debug|Any CPU.ActiveCfg = Debug|Any CPU");
            WriteLine($"		{apiProjectGuid}.Debug|Any CPU.Build.0 = Debug|Any CPU");
            WriteLine($"		{apiProjectGuid}.Release|Any CPU.ActiveCfg = Release|Any CPU");
            WriteLine($"		{apiProjectGuid}.Release|Any CPU.Build.0 = Release|Any CPU");
            WriteLine("	EndGlobalSection");
            WriteLine("	GlobalSection(SolutionProperties) = preSolution");
            WriteLine("		HideSolutionNode = FALSE");
            WriteLine("	EndGlobalSection");
            WriteLine("EndGlobal");
            return GetText();
        }

        private static string GenerateGuid() =>
            Guid.NewGuid().ToString("B").ToUpper();
    }
}
