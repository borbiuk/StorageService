using API.TransferData;
using AutoMapper;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace API.Services
{
	public class DataAccessService : IDataAccessService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public DataAccessService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uow = unitOfWork;
			_mapper = mapper;
		}

		private static DateTime CurrentTime { get => DateTime.Now; }

		public long SaveData(string data)
		{
			var entity = new BaseEntity
			{
				Data = data,
				Date = CurrentTime,
			};

			_uow.Entities.AddOrUpdate(entity);
			_uow.Commit();

			return entity.Id;
		}

		public async Task<long> SaveDataAsync(string data)
		{
			var entity = new BaseEntity
			{
				Data = data,
				Date = CurrentTime
			};

			await _uow.Entities.AddOrUpdateAsync(entity);
			await _uow.CommitAsync();

			return entity.Id;
		}

		public BaseEntityDto GetData(long id)
		{
			var entity = _uow.Entities.Get(id);
			var dto = entity != null ? _mapper.Map<BaseEntityDto>(entity) : null;
			return dto;
		}
	}
}
