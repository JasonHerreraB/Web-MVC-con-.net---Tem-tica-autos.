using System.ComponentModel.DataAnnotations.Schema;

namespace guia_2.Models;

public class Tiposdeautos {
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Image { get; set; }
    public string? Descripcion { get; set; }
    public List<Imagesauto>? Images { get; set; }
}