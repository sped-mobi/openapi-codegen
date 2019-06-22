// -----------------------------------------------------------------------
// <copyright file="ICoreProjectScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ICoreProjectScaffolder
    {
        void Save(CoreProjectModel model);

        CoreProjectModel ScaffoldModel(OpenApiOptions options);
    }
}
