using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace guia_2.Models;

public class ImagesAutos{
    public int Id { get; set; }
    public String? Url { get; set; }
    public int AutosId { get; set; }
    public Autos? Autos { get; set; }
}