using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TDDPalestraXUnitExample3.ErroHelpers;
using TDDPalestraXUnitExample3.Extension;
using TDDPalestraXUnitExample3.Helpers;
using TDDPalestraXUnitExample3.Model;

namespace TDDPalestraXUnitExample3.Validator
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithCustomError(AccountBusinessError.Account_NameIsMandatory);
            RuleFor(r => r.Name).MaximumLength(100).WithCustomError(AccountBusinessError.Account_NameExceededMaxLength, 100);

            When(w => !string.IsNullOrEmpty(w.ReferenceId), () =>
            {
                RuleFor(r => r.ReferenceId).MaximumLength(50).WithCustomError(AccountBusinessError.Account_ReferenceIdExceededMaxLength, 50);
            });

            RuleFor(r => r.BirthDate).NotEmpty().WithCustomError(AccountBusinessError.Account_BirthDateIsMandatory);

            RuleFor(r => r.BirthDate).Must(x => x < DateTime.Now).WithCustomError(AccountBusinessError.Account_BirthDateCannotBeFutureDate);
            RuleFor(r => r.BirthDate).Must(x => x > new DateTime(1900, 01, 01).AddDays(-1)).Unless(u => u.BirthDate == default(DateTime)).WithCustomError(AccountBusinessError.Account_BirthDateCannotBeLongPast);

            RuleFor(r => r.Login).NotEmpty().WithCustomError(AccountBusinessError.Account_LoginIsMandatory);
            RuleFor(r => r.Login).MaximumLength(50).WithCustomError(AccountBusinessError.Account_LoginExceededMaxLength, 50);

            RuleFor(r => r.Password).NotEmpty().WithCustomError(AccountBusinessError.Account_PasswordIsMandatory);
            RuleFor(r => r.Password).MaximumLength(200).WithCustomError(AccountBusinessError.Account_PasswordExceededMaxLength, 200);

            When(w => !string.IsNullOrEmpty(w.DocumentCPF), () =>
            {
                RuleFor(r => r.DocumentCPF).Must(v => ValidationHelpers.ValidateCPF(v)).WithCustomError(AccountBusinessError.Account_DocumentCPFIsInvalid);
            });

            When(w => !string.IsNullOrEmpty(w.DocumentRG), () =>
            {
                RuleFor(r => r.DocumentRG).Must(v => ValidationHelpers.IsRG(v)).WithCustomError(AccountBusinessError.Account_DocumentRGIsInvalid);
            });

            When(w => !string.IsNullOrEmpty(w.CellPhoneNumber), () =>
            {
                RuleFor(r => r.CellPhoneNumber).Must(v => ValidationHelpers.ValidatePhone(v)).WithCustomError(AccountBusinessError.Account_CellphoneNumberIsInvalid);
            });

            When(w => !string.IsNullOrEmpty(w.Email), () =>
            {
                RuleFor(r => r.Email).Must(v => ValidationHelpers.ValidateEmail(v)).WithCustomError(AccountBusinessError.Account_EmailIsInvalid);
            });
        }
    }
}
