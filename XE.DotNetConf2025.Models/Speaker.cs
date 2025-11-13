namespace XE.DotNetConf2025.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a speaker with identifying information such as name and email address.
/// </summary>
/// <remarks>The Speaker class is typically used to model individuals participating in events, conferences, or
/// presentations. Both the Name and Email properties are required and must meet validation constraints. This type does
/// not provide behavior beyond property storage.</remarks>
public class Speaker : IValidatableObject
{
    /// <summary>
    /// Gets or sets the name associated with the entity.
    /// </summary>
    /// <remarks>The name must be between 2 and 100 characters in length. This property is required and cannot
    /// be null or empty.</remarks>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the email address associated with the user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Name cannot be equal to "Pippo"
        if (string.Compare(Name, "pippo", true) == 0)
        {
            yield return new ValidationResult(
                "The name 'Pippo' is not allowed.",
                new[] { nameof(Name) });
        }
    }
}
