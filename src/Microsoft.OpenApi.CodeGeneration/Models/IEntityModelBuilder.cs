// -----------------------------------------------------------------------
// <copyright file="IEntityModelBuilder.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public interface IEntityModelBuilder
    {
        EntityModel BuildModel(OpenApiOptions options);
    }
}
