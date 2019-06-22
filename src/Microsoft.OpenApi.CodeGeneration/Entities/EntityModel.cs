// -----------------------------------------------------------------------
// <copyright file="EntityModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public class EntityModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
