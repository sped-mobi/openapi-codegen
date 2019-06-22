// -----------------------------------------------------------------------
// <copyright file="IContextScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public interface IContextScaffolder
    {
        void Save(ContextModel model);

        ContextModel ScaffoldModel(OpenApiOptions options);
    }
}
