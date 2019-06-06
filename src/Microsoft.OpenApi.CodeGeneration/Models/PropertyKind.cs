namespace Microsoft.OpenApi.CodeGeneration.Models
{
    public enum PropertyKind
    {
        Normal,
        PrimaryKey,
        ForeignKey,
        CollectionNavigation,
        ReferenceNavigation,
    }
}