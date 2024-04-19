using AutoMapper;
using CQRS.Application.Commands.User;
using CQRS.Application.Requests.User;
using CQRS.Data;

namespace CQRS.Application.AutoMapper
{
    public class ViewModelToDTO : Profile
    {
        public ViewModelToDTO()
        {
            CreateMap<CommandAddUser, UserDTO>();
            CreateMap<CommandUpdateUser, UserDTO>().ForMember(m => m.Id, dst => dst.MapFrom(d => Guid.Parse(d.Id)));
            CreateMap<AddUserRequest, UserDTO>();
        }
    }
}
