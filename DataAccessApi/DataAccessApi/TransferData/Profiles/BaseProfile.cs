using API.Services;
using AutoMapper;
using DAL.Entities;
using DAL.UnitOfWork;

namespace API.TransferData.Profiles
{
	public class BaseProfile : Profile
	{
		public BaseProfile()
		{
			CreateMap<BaseEntityDto, BaseEntity>();
			CreateMap<BaseEntity, BaseEntityDto>();
		}
	}
}
