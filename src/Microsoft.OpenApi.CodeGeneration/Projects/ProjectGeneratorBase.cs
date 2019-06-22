// -----------------------------------------------------------------------
// <copyright file="ProjectGeneratorBase.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public abstract class ProjectGeneratorBase : AbstractGenerator
    {
        protected ProjectGeneratorBase(GeneratorDependencies dependencies) : base(dependencies)
        {
        }

        public abstract string WriteProjectFile(OpenApiOptions options);

        protected void WriteSdkElement(string name)
        {
            WriteLine($"<Sdk Name=\"{name}\" />");
        }

        protected void WriteProperty(string name, string value)
        {
            WriteLine($"<{name}>{value}</{name}>");
        }

        protected void WriteItem(string name, string include)
        {
            WriteLine($"<{name} Include=\"{include}\" />");
        }
    }
}
