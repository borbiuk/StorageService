using DAL.Entities;

namespace DataAccess.Entities
{
	public class UserSoftEntity : IEntity
	{
		public long Id { get; set; }

		public long UserId { get; set; }

		public UserEntity User { get; set; }

		public long SoftId { get; set; }

		public SoftEntity Soft { get; set; }
	}
}
