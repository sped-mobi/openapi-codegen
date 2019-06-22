// -----------------------------------------------------------------------
// <copyright file="IControllerScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Controllers
{
    public interface IControllerScaffolder
    {
        void Save(ControllerModel model);

        ControllerModel ScaffoldModel(OpenApiOptions options);
    }
}
