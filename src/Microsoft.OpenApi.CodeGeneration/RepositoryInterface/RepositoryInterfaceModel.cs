// -----------------------------------------------------------------------
// <copyright file="RepositoryInterfaceModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.RepositoryInterface
{
    public class RepositoryInterfaceModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
