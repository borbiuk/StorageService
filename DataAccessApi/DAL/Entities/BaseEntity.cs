using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
	/// <summary>
	/// Simple entity.
	/// </summary>
	[Table("base")]
	public class BaseEntity : IEntity
	{
		[Column("id")]
		[ScaffoldColumn(false)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[Column("data")]
		[MaxLength(1_000)]
		public string Data { get; set; }

		[Column("date")]
		public DateTime Date { get; set; }
	}
}
