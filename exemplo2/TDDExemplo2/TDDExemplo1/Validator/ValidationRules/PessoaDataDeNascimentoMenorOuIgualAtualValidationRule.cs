using System;
using System.Collections.Generic;
using System.Text;
using TDDExemplo1.Models;

namespace TDDExemplo1.Validator.ValidationRules
{
    public class PessoaDataDeNascimentoMenorOuIgualAtualValidationRule
    {

        private readonly Pessoa _pessoa;
        public PessoaDataDeNascimentoMenorOuIgualAtualValidationRule(Pessoa pess)
        {
            _pessoa = pess;
        }


        public bool IsValid()
        {
            if (_pessoa.DataDeNascimento <= DateTime.Today)
                return true;

            return false;
        }
    }
}
