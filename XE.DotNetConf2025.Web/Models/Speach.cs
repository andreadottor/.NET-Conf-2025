namespace XE.DotNetConf2025.Web.Models;

using System.ComponentModel.DataAnnotations;

[ValidatableType]
public class Speach
{
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string? Title { get; set; }

    public string? Abstract { get; set; }

    public Speaker Speaker { get; set; } = new();
}
