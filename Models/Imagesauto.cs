using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_2.Models;

public class Imagesauto{
    public int Id { get; set; }
    public String? url { get; set; }
    public int TiposdeautosId { get; set; }
    public Tiposdeautos? Tiposdeauto { get; set; }
}