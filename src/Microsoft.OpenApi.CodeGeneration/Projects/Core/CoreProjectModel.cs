// -----------------------------------------------------------------------
// <copyright file="CoreProjectModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration.Projects
{
    public class CoreProjectModel
    {
        public ScaffoldedFile ProjectFile { get; set; }

        public IReadOnlyList<ScaffoldedFile> Converters { get; set; }

        public IReadOnlyList<ScaffoldedFile> Entities { get; set; }

        public IReadOnlyList<ScaffoldedFile> ViewModels { get; set; }

        public IReadOnlyList<ScaffoldedFile> RepositoryInterfaces { get; set; }

        public ScaffoldedFile Supervisor { get; set; }

        public ScaffoldedFile SupervisorInterface { get; set; }
    }
}
