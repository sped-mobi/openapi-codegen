// -----------------------------------------------------------------------
// <copyright file="ISolutionService.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionFactory
    {
        void CreateSolution(bool open);
    }
}
