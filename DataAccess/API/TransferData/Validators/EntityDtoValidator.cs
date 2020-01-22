using FluentValidation;
using System;

namespace API.TransferData.Validators
{
	public class EntityDtoValidator : AbstractValidator<EntityDto>
	{
		public EntityDtoValidator()
		{
			RuleFor(_ => _.Id)
				.GreaterThan(0);

			RuleFor(_ => _.Date)
				.LessThanOrEqualTo(DateTime.Now);

			RuleFor(_ => _.Data)
				.NotEmpty()
				.MaximumLength(1000);
		}
	}
}
