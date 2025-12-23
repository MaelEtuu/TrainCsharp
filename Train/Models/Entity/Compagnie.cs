using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Train.Models.Entity;

[Table("t_e_compagnie_cmp")]
public class Compagnie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("cmp_id")]
    public int IdCompagnie { get; set; }

    [Required]
    [StringLength(150)]
    [Column("cmp_nom")]
    public string Nom { get; set; } = null!;

    [Required]
    [StringLength(200)]
    [Column("cmp_siege_social")]
    public string SiegeSocial { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    [Column("cmp_email")]
    public string Email { get; set; } = null!;

    [Column("cmp_date_creation")]
    public DateTime DateCreation { get; set; }

    // Navigation
    [InverseProperty(nameof(Train.CompagnieNav))]
    public virtual ICollection<Train> Trains { get; set; } = new List<Train>();
}