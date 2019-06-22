// -----------------------------------------------------------------------
// <copyright file="IRepositoryScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Repository
{
    public interface IRepositoryScaffolder
    {
        void Save(RepositoryModel model);

        RepositoryModel ScaffoldModel(OpenApiOptions options);
    }
}
