// -----------------------------------------------------------------------
// <copyright file="ISolutionProjectScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionScaffolder
    {
        void Save(SolutionModel model);

        SolutionModel ScaffoldModel(OpenApiOptions options);
    }
}
