using System.Text.Json.Serialization;

namespace SBCW.DAL.Models;

public class Tag
{
    public Guid Id { get; set; }
    public string Word { get; set; } = null!;
    
    [JsonIgnore] public ICollection<ProductTag>? ProductTags { get; set; } = null!;
}