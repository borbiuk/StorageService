using System.Collections.Generic;
using DAL.Entities;

namespace DataAccess.Entities
{
	public class SoftwareEntity : IEntity
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserSoftwareEntity> UserSoftwareEntities { get; set; }
	}
}
