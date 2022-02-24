using AutoMapper;

namespace UsedCar.Backend.Presentations.Functions.Users.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserUpdateRequest, UseCases.Users.Models.UserUpdateRequest>();
        }
    }
}
