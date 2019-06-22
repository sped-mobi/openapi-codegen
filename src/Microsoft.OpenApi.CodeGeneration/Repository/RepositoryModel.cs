// -----------------------------------------------------------------------
// <copyright file="RepositoryModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Repository
{
    public class RepositoryModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
