using System.ComponentModel.DataAnnotations.Schema;

namespace guia_2.Models;

public class Autos{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Year { get; set; }
    public string? Origin { get; set; }
    public string? Weight { get; set; }
    public string? Acceleration { get; set; }
    public string? Poster { get; set; }
    public string? Description { get; set; }
    public List<ImagesAutos>? Images { get; set; }

    [NotMapped]
    public string[]? Imagenes { get; set; }
}