using AutoMapper;
using DAL.Entities;

namespace API.TransferData.Profiles
{
	internal class BaseProfile : Profile
	{
		public BaseProfile()
		{
			CreateMap<EntityDto, SimpleEntity>();
			CreateMap<SimpleEntity, EntityDto>();
		}
	}
}
