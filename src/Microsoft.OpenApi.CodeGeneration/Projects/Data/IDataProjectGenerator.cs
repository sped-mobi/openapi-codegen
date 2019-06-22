// -----------------------------------------------------------------------
// <copyright file="IDataProjectGenerator.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IDataProjectGenerator
    {
        string WriteProjectFile(OpenApiOptions options);
    }
}
