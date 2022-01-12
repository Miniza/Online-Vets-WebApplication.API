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
            CreateMap<UpdateOwner, DataModels.Owner>();
            CreateMap<UpdatePet, DataModels.Pet>();
            CreateMap<AddNewOwner, DataModels.Owner>();
            CreateMap<AddNewPet, DataModels.Pet>();
        }
    }
}
