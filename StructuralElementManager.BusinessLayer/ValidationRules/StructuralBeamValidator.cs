using FluentValidation;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.ValidationRules
{
    public class StructuralBeamValidator : AbstractValidator<StructuralBeam>
    {
        public StructuralBeamValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Beam name is required")
                .MaximumLength(50).WithMessage("Beam name cannot exceed 50 characters");

            RuleFor(x => x.Length)
                .GreaterThan(0).WithMessage("Length must be greater than 0")
                .LessThanOrEqualTo(1000).WithMessage("Length cannot exceed 1000 cm");

            RuleFor(x => x.Width)
                .GreaterThan(0).WithMessage("Width must be greater than 0")
                .LessThanOrEqualTo(200).WithMessage("Width cannot exceed 200 cm");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0")
                .LessThanOrEqualTo(200).WithMessage("Height cannot exceed 200 cm");

            RuleFor(x => x.MaterialID)
                .GreaterThan(0).WithMessage("Material must be selected");

        }
    }
}
