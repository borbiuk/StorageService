using DAL.Entities;

namespace DataAccess.Entities
{
	public class UserSoftwareEntity : IEntity
	{
		public long Id { get; set; }

		public long UserId { get; set; }

		public UserEntity User { get; set; }

		public long SoftwareId { get; set; }

		public SoftwareEntity Software { get; set; }
	}
}
