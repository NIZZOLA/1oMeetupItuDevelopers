using System;
using System.Collections.Generic;
using System.Text;

namespace TDDExemplo1.Models
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; }

        public DateTime DataDeNascimento { get; set; }

        public string Sexo { get; set; }
    }
}
