namespace Train.Shared.DTO;

public class CompagnieDTO
{
    public int IdCompagnie { get; set; }
    public string Nom { get; set; } = null!;
    public string SiegeSocial { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DateCreation { get; set; }
}

public class CompagnieDetailDTO
{
    public int IdCompagnie { get; set; }
    public string Nom { get; set; } = null!;
    public string SiegeSocial { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime DateCreation { get; set; }
    public int NombreTrains { get; set; }
    public int NombreTrainsActifs { get; set; }
}

public class CompagnieCreateDTO
{
    public string Nom { get; set; } = null!;
    public string SiegeSocial { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class CompagnieUpdateDTO
{
    public int IdCompagnie { get; set; }
    public string Nom { get; set; } = null!;
    public string SiegeSocial { get; set; } = null!;
    public string Email { get; set; } = null!;
}