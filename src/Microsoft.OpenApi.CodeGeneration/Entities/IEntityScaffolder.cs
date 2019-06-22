// -----------------------------------------------------------------------
// <copyright file="IEntityScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Entities
{
    public interface IEntityScaffolder
    {
        void Save(EntityModel model);

        EntityModel ScaffoldModel(OpenApiOptions options);
    }
}
