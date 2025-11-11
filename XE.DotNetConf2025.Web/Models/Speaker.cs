namespace XE.DotNetConf2025.Web.Models;

using System.ComponentModel.DataAnnotations;

public class Speaker
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}
