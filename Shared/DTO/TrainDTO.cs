namespace Train.Shared.DTO;

public class TrainDTO
{
    public int IdTrain { get; set; }
    public string Nom { get; set; } = null!;
    public int Capacite { get; set; }
    public int IdCompagnie { get; set; }
    public string NomCompagnie { get; set; } = null!;
    public DateTime DateMiseEnService { get; set; }
    public bool EstActif { get; set; }
}

public class TrainDetailDTO
{
    public int IdTrain { get; set; }
    public string Nom { get; set; } = null!;
    public int Capacite { get; set; }
    public int IdCompagnie { get; set; }
    public string NomCompagnie { get; set; } = null!;
    public string EmailCompagnie { get; set; } = null!;
    public DateTime DateMiseEnService { get; set; }
    public bool EstActif { get; set; }
    public int NombreVoyages { get; set; }
}

public class TrainCreateDTO
{
    public string Nom { get; set; } = null!;
    public int Capacite { get; set; }
    public int IdCompagnie { get; set; }
    public DateTime DateMiseEnService { get; set; }
}

public class TrainUpdateDTO
{
    public int IdTrain { get; set; }
    public string Nom { get; set; } = null!;
    public int Capacite { get; set; }
    public bool EstActif { get; set; }
}