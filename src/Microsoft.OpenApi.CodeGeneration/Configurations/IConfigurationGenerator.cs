// -----------------------------------------------------------------------
// <copyright file="IConfigurationGenerator.cs" company="Brad Marshall">
//     Copyright � 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public interface IConfigurationGenerator
    {
        string WriteCode(OpenApiSchema schema, string name, string @namespace);
    }
}
