using System.Collections.Generic;
using DataAccess.Entities;

namespace DAL.Entities
{
	/// <summary>
	/// Simple entity.
	/// </summary>
	public class UserEntity : IEntity
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public ICollection<UserSoftEntity> UserSoftEntities { get; set; }
	}
}
