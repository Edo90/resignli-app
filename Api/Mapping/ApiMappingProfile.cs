using Api.Dtos;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Api.Mapping
{
	public class ApiMappingProfile : Profile
	{
		public ApiMappingProfile()
		{
			CreateMap<Employee, EmployeeDto>()
				.ForMember(dest => dest.LastUpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt)).ReverseMap();

			CreateMap<CreateEmployeeDto, Employee>();
			CreateMap<UpdateEmployeeDto, Employee>()
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

			CreateMap<UserApp, UserDto>()
				.ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.UpdatedAt))
				.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.CreatedAt));
		}
	}
}
