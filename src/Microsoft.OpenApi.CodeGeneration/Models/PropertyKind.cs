// -----------------------------------------------------------------------
// <copyright file="PropertyKind.cs" company="Brad Marshall">
//     Copyright © 2019 Brad Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public enum PropertyKind
    {
        Normal,
        PrimaryKey,
        ForeignKey,
        CollectionNavigation,
        ReferenceNavigation
    }
}
