// -----------------------------------------------------------------------
// <copyright file="ISupervisorScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Supervisor
{
    public interface ISupervisorScaffolder
    {
        void Save(SupervisorModel model);

        SupervisorModel ScaffoldModel(OpenApiOptions options);
    }
}
