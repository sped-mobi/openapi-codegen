// -----------------------------------------------------------------------
// <copyright file="ConfigurationModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public class ConfigurationModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
