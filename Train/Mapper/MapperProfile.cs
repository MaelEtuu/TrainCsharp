using AutoMapper;
using Train.Models.Entity;
using Train.Shared.DTO;

namespace Train.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Train mappings
        CreateMap<Train.Models.Entity.Train, TrainDTO>()
            .ForMember(dest => dest.NomCompagnie,
                opt => opt.MapFrom(src => src.CompagnieNav.Nom))
            .ReverseMap();

        CreateMap<Train.Models.Entity.Train, TrainDetailDTO>()
            .ForMember(dest => dest.NomCompagnie,
                opt => opt.MapFrom(src => src.CompagnieNav.Nom))
            .ForMember(dest => dest.EmailCompagnie,
                opt => opt.MapFrom(src => src.CompagnieNav.Email))
            .ForMember(dest => dest.NombreVoyages,
                opt => opt.MapFrom(src => src.Voyages.Count))
            .ReverseMap();

        CreateMap<TrainCreateDTO, Train.Models.Entity.Train>()
            .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => true))
            .ReverseMap();

        CreateMap<TrainUpdateDTO, Train.Models.Entity.Train>()
            .ReverseMap();

        // Compagnie mappings
        CreateMap<Compagnie, CompagnieDTO>()
            .ReverseMap();

        CreateMap<Compagnie, CompagnieDetailDTO>()
            .ForMember(dest => dest.NombreTrains,
                opt => opt.MapFrom(src => src.Trains.Count))
            .ForMember(dest => dest.NombreTrainsActifs,
                opt => opt.MapFrom(src => src.Trains.Count(t => t.EstActif)))
            .ReverseMap();

        CreateMap<CompagnieCreateDTO, Compagnie>()
            .ForMember(dest => dest.DateCreation,
                opt => opt.MapFrom(src => DateTime.UtcNow))
            .ReverseMap();

        CreateMap<CompagnieUpdateDTO, Compagnie>()
            .ReverseMap();
    }
}