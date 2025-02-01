using AutoMapper;
using Model.Dto.Menu;
using Model.Dto.Role;
using Model.Dto.User;
using Model.Entitys;

namespace webapi.Config
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            //Role
            CreateMap<RoleAdd, Role>();
            CreateMap<RoleEdit, Role>();
            //User
            CreateMap<UserAdd, Users>();
            CreateMap<UserEdit, Users>();
            //Menu
            CreateMap<MenuAdd, Menu>();
            CreateMap<MenuEdit, Users>();
        }
    }
}
