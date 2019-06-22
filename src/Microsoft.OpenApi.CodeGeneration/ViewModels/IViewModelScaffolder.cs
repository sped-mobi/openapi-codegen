// -----------------------------------------------------------------------
// <copyright file="IViewModelScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.ViewModels
{
    public interface IViewModelScaffolder
    {
        void Save(ViewModelModel model);

        ViewModelModel ScaffoldModel(OpenApiOptions options);
    }
}
