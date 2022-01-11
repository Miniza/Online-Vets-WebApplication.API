using AutoMapper;
using OnlineVetAPI.DomainModels;

namespace OnlineVetAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Owner, Owner>().ReverseMap();
            CreateMap<DataModels.Pet, Pet>().ReverseMap();
        }
    }
}
