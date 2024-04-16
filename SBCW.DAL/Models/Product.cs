using System.Text.Json.Serialization;

namespace SBCW.DAL.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }

    public Type Type { get; set; } = null!;

    [JsonIgnore] public ICollection<ProductTag>? ProductTags { get; set; } = null!;
}