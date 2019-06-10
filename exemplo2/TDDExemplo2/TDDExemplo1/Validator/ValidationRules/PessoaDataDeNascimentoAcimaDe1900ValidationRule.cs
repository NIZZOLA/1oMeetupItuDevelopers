using System;
using System.Collections.Generic;
using System.Text;
using TDDExemplo1.Models;

namespace TDDExemplo1.Validator.ValidationRules
{
    public class PessoaDataDeNascimentoAcimaDe1900ValidationRule
    {
        private readonly Pessoa _pessoa;
        public PessoaDataDeNascimentoAcimaDe1900ValidationRule(Pessoa pess)
        {
            _pessoa = pess;
        }

        public bool IsValid()
        {
            if (_pessoa.DataDeNascimento > DateTime.Parse("01/01/1900"))
                return true;

            return false;
        }

    }
}
