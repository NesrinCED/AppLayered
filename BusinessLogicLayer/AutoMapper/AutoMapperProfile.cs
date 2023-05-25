using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using AutoMapper;
namespace AppAPI.AutoMapper
{
    public class AutoMapperProfile : Profile
    { 
        public AutoMapperProfile()
        {

            CreateMap<UpdateUserByAdminDTO, Employee>();

            CreateMap<Employee, UpdateUserByAdminDTO>();

            CreateMap<AuthenticateEmployeeDTO, Employee>();

            CreateMap<Employee, AuthenticateEmployeeDTO>();

            CreateMap<CreateProjectAuthorizationDTO, ProjectAuthorization>();

            CreateMap<ProjectAuthorization, CreateProjectAuthorizationDTO>();

            CreateMap<ProjectAuthorizationDTO, ProjectAuthorization>();

            CreateMap<ProjectAuthorization, ProjectAuthorizationDTO>();

            CreateMap<UpdateEmployeeDTO, Employee>();

            CreateMap<Employee, UpdateEmployeeDTO>();

            CreateMap<RoleDTO, Role>();

            CreateMap<Role, RoleDTO>();

            CreateMap<TemplateHistoryDTO, Templatehistory>();

            CreateMap<Templatehistory, TemplateHistoryDTO>();

            CreateMap<CreateRoleDTO, Role>();

            CreateMap<Role, CreateRoleDTO>();

            CreateMap<CreateTemplateHistoryDTO, Templatehistory>();

            CreateMap<Templatehistory, CreateTemplateHistoryDTO>();

            CreateMap<CreateProjectDTO, Project>();

            CreateMap<Project, CreateProjectDTO>();

            CreateMap<FilteredTemplatesProjectDTO, Template>()
              /*  .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.TemplateCreatedBy, opt => opt.MapFrom(src => src.TemplateCreatedBy))
                .ForMember(dest => dest.TemplateModifiedBy, opt => opt.MapFrom(src => src.TemplateModifiedBy))*/;
            

            CreateMap<Template,FilteredTemplatesProjectDTO>()
                /*.ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.TemplateCreatedBy, opt => opt.MapFrom(src => src.TemplateCreatedBy))
                .ForMember(dest => dest.TemplateModifiedBy, opt => opt.MapFrom(src => src.TemplateModifiedBy))*/;


            CreateMap<CreateEmployeeDTO, Employee>();

            CreateMap<Employee, CreateEmployeeDTO>();

            CreateMap<CreateTemplateDTO, Template>();

            CreateMap<Template, CreateTemplateDTO>();

            CreateMap<UpdateTemplateDTO, Template>();

            CreateMap<Template, UpdateTemplateDTO>();

            CreateMap<EmployeeDTO, Employee>();
              
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Project, ProjectDTO>();

            CreateMap<ProjectDTO, Project>();
            //CreateMap<Tsource, Tdestination>()
            CreateMap<TemplateDTO, Template>()
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.TemplateCreatedBy, opt => opt.MapFrom(src => src.TemplateCreatedBy))
                .ForMember(dest => dest.TemplateModifiedBy, opt => opt.MapFrom(src => src.TemplateModifiedBy));


            CreateMap<Template, TemplateDTO>()
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
                .ForMember(dest => dest.TemplateCreatedBy, opt => opt.MapFrom(src => src.TemplateCreatedBy))
                .ForMember(dest => dest.TemplateModifiedBy, opt => opt.MapFrom(src => src.TemplateModifiedBy));

        }
    }
}
