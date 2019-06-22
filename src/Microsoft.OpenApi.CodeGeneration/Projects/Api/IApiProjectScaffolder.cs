// -----------------------------------------------------------------------
// <copyright file="IApiProjectScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IApiProjectScaffolder
    {
        void Save(ApiProjectModel model);

        ApiProjectModel ScaffoldModel(OpenApiOptions options);
    }
}
