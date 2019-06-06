// -----------------------------------------------------------------------
// <copyright file="ISolutionService.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public interface ISolutionFactory
    {
        IOpenApiDocument Document { get; }

        void CreateSolution(bool openInExporer);
    }
}
