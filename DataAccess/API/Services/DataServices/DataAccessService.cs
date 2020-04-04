using API.TransferData;

using AutoMapper;

using DAL.Entities;
using DAL.UnitOfWork;

using System;
using System.Threading.Tasks;

namespace API.Services.DataServices
{
	internal class DataAccessService : IDataAccessService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public DataAccessService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_uow = unitOfWork;
			_mapper = mapper;
		}

		private static DateTime CurrentTime { get => DateTime.UtcNow; }

		public async Task<EntityDto> GetDataAsync(long id)
		{
			var entity = await _uow.Entities.Get(id);

			return entity != null
				? _mapper.Map<EntityDto>(entity)
				: null;
		}
		public async Task RemoveDataAsync(long id)
		{
			await _uow.Entities.Delete(id);
			await _uow.CommitAsync();
		}

		public async Task<long> SaveDataAsync(string data)
		{
			var entity = new SimpleEntity
			{
				Data = data,
				Date = CurrentTime,
			};

			await _uow.Entities.AddOrUpdate(entity);
			await _uow.CommitAsync();

			return entity.Id;
		}

		public async Task UpdateDataAsync(UpdateEntityDto dto)
		{
			var entity = _mapper.Map<SimpleEntity>(dto);

			await _uow.Entities.AddOrUpdate(entity);
			await _uow.CommitAsync();
		}
	}
}
