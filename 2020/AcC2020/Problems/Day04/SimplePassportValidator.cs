using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentValidation;

namespace AoC.AoC2020.Problems.Day04
{
    public class SimplePassportValidator : AbstractValidator<Passport>
    {

        public SimplePassportValidator()
        {
            RuleFor(p => p.BirthYear)
                .NotNull();

            RuleFor(p => p.IssueYear)
                .NotNull();

            RuleFor(p => p.ExpirationYear)
                .NotNull();
            
            // Validate height
            RuleFor(p => p.Height)
                .NotNull();

            RuleFor(p => p.HairColor)
                .NotEmpty();
            
            RuleFor(p => p.PassportId)
                .NotEmpty();

            RuleFor(p => p.EyeColor)
                .NotEmpty();
        }
    }
}
