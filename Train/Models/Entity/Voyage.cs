using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Train.Models.Entity;

[Table("t_e_voyage_vyg")]
public class Voyage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("vyg_id")]
    public int IdVoyage { get; set; }

    [Required]
    [Column("trn_id")]
    public int IdTrain { get; set; }

    [Required]
    [StringLength(100)]
    [Column("vyg_ville_depart")]
    public string VilleDepart { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Column("vyg_ville_arrivee")]
    public string VilleArrivee { get; set; } = null!;

    [Column("vyg_date_depart")]
    public DateTime DateDepart { get; set; }

    [Column("vyg_date_arrivee")]
    public DateTime DateArrivee { get; set; }

    [Column("vyg_prix")]
    public decimal Prix { get; set; }

    // Navigation
    [ForeignKey(nameof(IdTrain))]
    [InverseProperty(nameof(Train.Voyages))]
    public virtual Train TrainNav { get; set; } = null!;
}