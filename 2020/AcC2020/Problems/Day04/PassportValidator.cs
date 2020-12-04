using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FluentValidation;

namespace AoC.AoC2020.Problems.Day04
{
    public class PassportValidator : AbstractValidator<Passport>
    {
        private readonly string[] _validEyeColors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

        public PassportValidator()
        {
            RuleFor(p => p.BirthYear)
                .InclusiveBetween(1920, 2002)
                .NotNull();

            RuleFor(p => p.IssueYear)
                .InclusiveBetween(2010, 2020)
                .NotNull();

            RuleFor(p => p.ExpirationYear)
                .InclusiveBetween(2020, 2030)
                .NotNull();

            RuleFor(p => p.HeightType)
                .NotNull()
                .IsInEnum();

            // Validate height in cm
            RuleFor(p => p.Height)
                .InclusiveBetween(150, 193)
                .When(h => h.HeightType == HeightUnit.cm);

            // Validate height in inches
            RuleFor(p => p.Height)
                .InclusiveBetween(59, 76)
                .When(h => h.HeightType == HeightUnit.inch);

            RuleFor(p => p.HairColor)
                .NotEmpty()
                .Length(7)
                .Matches("^#[0-9 a-f]{6}");  // # followed by 6 characters [0-9] or [a-f]


            RuleFor(p => p.PassportId)
                .NotEmpty()
                .Length(9)
                .Matches("^[0-9]{9}$");

            RuleFor(p => p.EyeColor)
                .NotNull()
                .Length(3)
                .Must(eyeColor => _validEyeColors.Contains(eyeColor));
        }
    }
}
