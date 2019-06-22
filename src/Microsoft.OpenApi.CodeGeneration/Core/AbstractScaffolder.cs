// -----------------------------------------------------------------------
// <copyright file="AbstractScaffolder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration
{
    public abstract class AbstractScaffolder
    {
        protected AbstractScaffolder(ScaffolderDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        protected ScaffolderDependencies Dependencies { get; }
    }
}
