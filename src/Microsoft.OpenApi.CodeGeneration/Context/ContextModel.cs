// -----------------------------------------------------------------------
// <copyright file="ContextModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Context
{
    public class ContextModel
    {
        public ScaffoldedFile Class { get; set; }

        public ScaffoldedFile Interface { get; set; }
    }
}
