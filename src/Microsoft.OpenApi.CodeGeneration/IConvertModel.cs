// -----------------------------------------------------------------------
// <copyright file="IConvertModel.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration
{
    public interface IConvertModel<TSource, TTarget>
    {
        TTarget Convert { get; }
    }
}
