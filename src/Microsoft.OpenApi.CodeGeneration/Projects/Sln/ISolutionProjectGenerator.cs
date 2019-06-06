// -----------------------------------------------------------------------
// <copyright file="ISolutionGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}

