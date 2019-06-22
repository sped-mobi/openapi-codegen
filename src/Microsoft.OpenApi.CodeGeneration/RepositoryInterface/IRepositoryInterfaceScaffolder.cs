// -----------------------------------------------------------------------
// <copyright file="IRepositoryInterfaceScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.RepositoryInterface
{
    public interface IRepositoryInterfaceScaffolder
    {
        void Save(RepositoryInterfaceModel model);

        RepositoryInterfaceModel ScaffoldModel(OpenApiOptions options);
    }
}
