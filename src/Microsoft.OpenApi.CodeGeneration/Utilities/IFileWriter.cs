// -----------------------------------------------------------------------
// <copyright file="IFileWriter.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IFileWriter
    {
        void WriteFile(ScaffoldedFile file);

        void WriteFiles(IEnumerable<ScaffoldedFile> files);
    }
}
