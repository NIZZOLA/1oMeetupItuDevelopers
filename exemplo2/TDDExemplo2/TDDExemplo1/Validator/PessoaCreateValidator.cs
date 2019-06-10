using System;
using System.Collections.Generic;
using System.Text;
using TDDExemplo1.Models;
using TDDExemplo1.Validator.ValidationRules;

namespace TDDExemplo1.Validator
{
    public class PessoaCreateValidator
    {
        public bool Validate( Pessoa pess)
        {
            Boolean resposta = true;

            if (!new PessoaDataDeNascimentoAcimaDe1900ValidationRule(pess).IsValid())
                resposta = false;

            if (!new PessoaDataDeNascimentoMenorOuIgualAtualValidationRule(pess).IsValid())
                resposta = false;

            return resposta;
        }
    }
}
