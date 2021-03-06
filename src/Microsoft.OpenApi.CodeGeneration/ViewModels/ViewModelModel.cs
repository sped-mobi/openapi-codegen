﻿// -----------------------------------------------------------------------
// <copyright file="ViewModelModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.ViewModels
{
    public class ViewModelModel
    {
        public IReadOnlyList<ScaffoldedFile> Files { get; set; }
    }
}
