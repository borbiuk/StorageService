using AutoMapper;
using DAL.Entities;

namespace API.TransferData.Profiles
{
	internal class SimpleEntityProfile : Profile
	{
		public SimpleEntityProfile()
		{
			CreateMap<SimpleEntity, EntityDto>();
			CreateMap<UpdateEntityDto, SimpleEntity>();
		}
	}
}
