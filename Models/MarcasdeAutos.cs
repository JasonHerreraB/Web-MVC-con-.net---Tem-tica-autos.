using System.ComponentModel.DataAnnotations.Schema;

namespace guia_2.Models;

public class MarcasdeAutos{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Logo { get; set; }
    public string? Created { get; set; }
    public string? Origin { get; set; }
    public string? Dercription { get; set; }

}