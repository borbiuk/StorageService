using System.Collections.Generic;
using DAL.Entities;

namespace DataAccess.Entities
{
	public class SoftEntity : IEntity
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserSoftEntity> UserSoftEntities { get; set; }
	}
}
