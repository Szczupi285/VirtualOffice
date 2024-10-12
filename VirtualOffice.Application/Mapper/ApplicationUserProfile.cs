using AutoMapper;
using VirtualOffice.Application.Models;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Application.Mapper
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, EmployeeReadModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src._Name.Value))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src._Surname.Value))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src._Permissions));

            CreateMap<EmployeeReadModel, ApplicationUser>()
                .ConstructUsing(src => new ApplicationUser(
                    Guid.Parse(src.Id),
                    new ApplicationUserName(src.Name),
                    new ApplicationUserSurname(src.Surname),
                    (PermissionsEnum)src.Permissions))
                .ForAllMembers(oth => oth.Ignore());
        }
    }
}