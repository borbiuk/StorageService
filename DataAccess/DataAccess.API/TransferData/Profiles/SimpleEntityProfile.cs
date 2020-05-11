using AutoMapper;
using DAL.Entities;

namespace API.TransferData.Profiles
{
	internal class SimpleEntityProfile : Profile
	{
		public SimpleEntityProfile()
		{
			CreateMap<DataEntity, EntityDto>();
			CreateMap<UpdateEntityDto, DataEntity>();
		}
	}
}
