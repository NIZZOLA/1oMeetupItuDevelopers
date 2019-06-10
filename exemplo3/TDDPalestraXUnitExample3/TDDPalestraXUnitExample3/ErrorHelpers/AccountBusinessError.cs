using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TDDPalestraXUnitExample3.ErroHelpers
{
    public enum AccountBusinessError
    {
        #region Account

        [Description("O campo Nome não deve ser vazio")]
        Account_NameIsMandatory,

        [Description("O campo Id Externo deve ter no máximo {0} caracteres")]
        Account_ReferenceIdExceededMaxLength,

        [Description("O campo Data de Nascimento não deve ser vazio/inválido")]
        Account_BirthDateIsMandatory,

        [Description("O campo Data de Nascimento não deve ser uma data futura")]
        Account_BirthDateCannotBeFutureDate,

        [Description("O campo Data de Nascimento não deve ser uma data anterior a 01/01/1900")]
        Account_BirthDateCannotBeLongPast,

        [Description("O campo Gênero deve ser informado")]
        Account_GenderIsMandatory,

        [Description("O campo Login não deve ser vazio")]
        Account_LoginIsMandatory,

        [Description("O campo Senha não deve ser vazio")]
        Account_PasswordIsMandatory,

        [Description("O tamanho máximo do campo Login é {0}")]
        Account_LoginExceededMaxLength,

        [Description("O tamanho máximo do campo Nome é {0}")]
        Account_NameExceededMaxLength,

        [Description("O formato do campo CPF é inválido")]
        Account_DocumentCPFIsInvalid,

        [Description("O formato do campo RG é inválido")]
        Account_DocumentRGIsInvalid,

        [Description("O tamanho máximo do campo Senha Criptografado é {0}")]
        Account_PasswordExceededMaxLength,

        [Description("O formato do campo Telefone não é válido")]
        Account_CellphoneNumberIsInvalid,

        [Description("O formato do campo Email não é válido")]
        Account_EmailIsInvalid,

        [Description("O usuário solicitado não existe")]
        Account_DoesNotExist,

        [Description("Não é permitido vincular um responsável mais de uma vez")]
        Account_AlreadyContainsParent,

        [Description("O Login deve ser único. Já existe outro usuário com o Login {0}")]
        Account_LoginMustBeUnique,

        [Description("O Id Externo deve ser único. Já existe outro usuário com o Id Externo {0}")]
        Account_ReferenceIdMustBeUnique,

        #endregion
    }
}
