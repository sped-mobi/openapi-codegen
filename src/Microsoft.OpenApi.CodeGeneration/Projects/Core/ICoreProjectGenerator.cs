// -----------------------------------------------------------------------
// <copyright file="ICoreProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ICoreProjectGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}
