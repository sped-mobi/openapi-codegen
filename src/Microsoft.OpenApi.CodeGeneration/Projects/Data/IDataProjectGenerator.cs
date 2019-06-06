// -----------------------------------------------------------------------
// <copyright file="IDataProjectGenerator.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IDataProjectGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}

