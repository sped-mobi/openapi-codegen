// -----------------------------------------------------------------------
// <copyright file="DataProjectModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class DataProjectModel
    {
        public ScaffoldedFile ProjectFile { get; set; }

        public ScaffoldedFile DataContext { get; set; }

        public IReadOnlyList<ScaffoldedFile> Configurations { get; set; }

        public IReadOnlyList<ScaffoldedFile> RepositoryClasses { get; set; }
    }
}
