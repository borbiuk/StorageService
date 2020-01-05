using System;

namespace DAL.Entities
{
	/// <summary>
	/// Simple entity.
	/// </summary>
	public class EntityBase : IEntity
	{
		public long Id { get; set; }

		public DateTime Date { get; set; }

		public string Data { get; set; }
	}
}
