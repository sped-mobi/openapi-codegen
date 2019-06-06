// -----------------------------------------------------------------------
// <copyright file="IOpenApiDocument.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert { get; }
    }
}
