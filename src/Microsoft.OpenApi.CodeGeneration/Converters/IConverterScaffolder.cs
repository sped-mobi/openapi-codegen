// -----------------------------------------------------------------------
// <copyright file="IConverterScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Converters
{
    public interface IConverterScaffolder
    {
        void Save(ConverterModel model);

        ConverterModel ScaffoldModel(OpenApiOptions options);
    }
}
