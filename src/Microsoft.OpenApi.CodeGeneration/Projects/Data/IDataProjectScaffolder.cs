// -----------------------------------------------------------------------
// <copyright file="IDataProjectScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface IDataProjectScaffolder
    {
        void Save(DataProjectModel model);

        DataProjectModel ScaffoldModel(OpenApiOptions options);
    }
}
