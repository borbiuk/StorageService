using System.Collections.Generic;

namespace DataAccess.API.Dto
{
	public class UserDto
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<long> Soft { get; set; }
	}
}
