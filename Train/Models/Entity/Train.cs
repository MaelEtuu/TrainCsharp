using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Train.Models.Entity;

[Table("t_e_train_trn")]
public class Train
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("trn_id")]
    public int IdTrain { get; set; }

    [Required]
    [StringLength(100)]
    [Column("trn_nom")]
    public string Nom { get; set; } = null!;

    [Required]
    [Column("trn_capacite")]
    public int Capacite { get; set; }

    [Required]
    [Column("cmp_id")]
    public int IdCompagnie { get; set; }

    [Column("trn_date_mise_service")]
    public DateTime DateMiseEnService { get; set; }

    [Column("trn_actif")]
    public bool EstActif { get; set; } = true;

    // Navigation
    [ForeignKey(nameof(IdCompagnie))]
    [InverseProperty(nameof(Compagnie.Trains))]
    public virtual Compagnie CompagnieNav { get; set; } = null!;

    [InverseProperty(nameof(Voyage.TrainNav))]
    public virtual ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}