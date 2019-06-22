// -----------------------------------------------------------------------
// <copyright file="ISolutionProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}
