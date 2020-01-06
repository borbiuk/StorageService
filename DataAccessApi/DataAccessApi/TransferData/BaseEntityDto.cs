using System;
using System.ComponentModel.DataAnnotations;

namespace API.TransferData
{
	public class BaseEntityDto
	{
		public long Id { get; set; }

		public DateTime Date { get; set; }

		[MaxLength(1_000)]
		public string Data { get; set; }
	}
}
