// -----------------------------------------------------------------------
// <copyright file="ICoreProjectGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ICoreProjectGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}

