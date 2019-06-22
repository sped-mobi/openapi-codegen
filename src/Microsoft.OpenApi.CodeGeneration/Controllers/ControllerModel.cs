// -----------------------------------------------------------------------
// <copyright file="ControllerModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public class ControllerModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
