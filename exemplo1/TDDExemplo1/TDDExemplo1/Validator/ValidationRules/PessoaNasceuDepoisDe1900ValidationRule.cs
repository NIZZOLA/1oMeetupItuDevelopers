using System;
using System.Collections.Generic;
using System.Text;
using TDDExemplo1.Models;

namespace TDDExemplo1.Validator.ValidationRules
{
    public class PessoaNasceuDepoisDe1900ValidationRule
    {
        private readonly Pessoa _pessoa;
        public PessoaNasceuDepoisDe1900ValidationRule(Pessoa pess)
        {
            _pessoa = pess;
        }

        public bool IsValid()
        {

            return false;
        }

    }
}
