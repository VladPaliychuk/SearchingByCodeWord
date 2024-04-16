using System.Text.Json.Serialization;

namespace SBCW.DAL.Models;

public class ProductTag
{
    public Guid ProductId { get; set; }
    public Guid TagId { get; set; }

    [JsonIgnore] public Product Product { get; set; } = null!;
    [JsonIgnore] public Tag Tag { get; set; } = null!;
}