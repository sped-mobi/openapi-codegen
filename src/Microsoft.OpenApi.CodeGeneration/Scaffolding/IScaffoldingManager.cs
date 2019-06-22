// -----------------------------------------------------------------------
// <copyright file="IScaffoldingManager.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Scaffolding
{
    public interface IScaffoldingManager
    {
        void SaveModel(ScaffoldedModel model);

        ScaffoldedModel ScaffoldModel(OpenApiOptions options);
    }
}
