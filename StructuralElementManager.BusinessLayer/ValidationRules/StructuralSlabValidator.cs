using FluentValidation;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.ValidationRules
{
    public class StructuralSlabValidator : AbstractValidator<StructuralSlab>
    {
        public StructuralSlabValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Column name is required")
                .MaximumLength(50).WithMessage("Column name cannot exceed 50 characters");

            RuleFor(x => x.Width)
                .GreaterThan(0).WithMessage("Width must be greater than 0")
                .LessThanOrEqualTo(200).WithMessage("Width cannot exceed 200 cm");

            RuleFor(x => x.FloorLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Floor level cannot be negative");

            RuleFor(x => x.MaterialID)
                .GreaterThan(0).WithMessage("Material must be selected");
        }
    }
}
