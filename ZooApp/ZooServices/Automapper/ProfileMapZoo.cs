using AutoMapper;
using DTO;
using ZooServices.Model;

namespace ZooServices.Automapper
{
    public class ProfileMapZoo : Profile
    {
        public ProfileMapZoo()
        {
            CreateMap<Animal, AnimalDto>()
                    .ForMember(dto => dto.Id, conf => conf.MapFrom(entity => entity.AnimIdPk))
                    .ForMember(dto => dto.IdBreed, conf => conf.MapFrom(entity => entity.BreedIdPk))
                    .ForMember(dto => dto.Name, conf => conf.MapFrom(entity => entity.AnimName))
                    .ForMember(dto => dto.Weight, conf => conf.MapFrom(entity => entity.AnimWeight))
                    .ForMember(dto => dto.Characteristics, conf => conf.MapFrom(entity => entity.AnimCharacteristics))
                    .ForMember(dto => dto.Age, conf => conf.MapFrom(entity => entity.AnimAge)); 

            CreateMap<AnimalDto, Animal>()
                .ForMember(entity => entity.AnimIdPk, conf => conf.MapFrom(dto => dto.Id))
                .ForMember(entity => entity.BreedIdPk, conf => conf.MapFrom(dto => dto.IdBreed))
                .ForMember(entity => entity.AnimName, conf => conf.MapFrom(dto => dto.Name))
                .ForMember(entity => entity.AnimWeight, conf => conf.MapFrom(dto => dto.Weight))
                .ForMember(entity => entity.AnimCharacteristics, conf => conf.MapFrom(dto => dto.Characteristics))
                .ForMember(entity => entity.AnimAge, conf => conf.MapFrom(dto => dto.Age));
        }
    }
}
