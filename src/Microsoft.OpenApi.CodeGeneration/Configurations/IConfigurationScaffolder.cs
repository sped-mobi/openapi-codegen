// -----------------------------------------------------------------------
// <copyright file="IConfigurationScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Configurations
{
    public interface IConfigurationScaffolder
    {
        void Save(ConfigurationModel model);

        ConfigurationModel ScaffoldModel(OpenApiOptions options);
    }
}
