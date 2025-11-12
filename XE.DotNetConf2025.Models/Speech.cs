namespace XE.DotNetConf2025.Models;

using Microsoft.Extensions.Validation;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a conference session, including its title, abstract, and associated speaker.
/// </summary>
/// <remarks>Use this class to model individual sessions within an event or conference schedule. The session title
/// must be between 5 and 200 characters in length. The associated speaker provides details about the presenter for the
/// session.</remarks>
[ValidatableType]
public class Speech
{
   /// <summary>
   /// Gets or sets the title of the item.
   /// </summary>
   /// <remarks>The title must be between 5 and 200 characters in length. This property is required and cannot
   /// be null or empty.</remarks>
    [Required]
    [StringLength(200, MinimumLength = 5)]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the abstract or summary text associated with the item.
    /// </summary>
    public string? Abstract { get; set; }

    /// <summary>
    /// Gets or sets the speaker associated with the current context.
    /// </summary>
    public Speaker Speaker { get; set; } = new();
}
