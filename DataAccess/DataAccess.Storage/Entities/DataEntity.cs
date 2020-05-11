using System;

namespace DAL.Entities
{
	/// <summary>
	/// Simple entity.
	/// </summary>
	public class DataEntity : IEntity
	{
		public long Id { get; set; }

		public string Data { get; set; }

		public DateTime Date { get; set; }
	}
}
