using System;
using TDDExemplo1.Models;
using TDDExemplo1.Validator;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        [Fact( DisplayName = "Classe completa válida")]
        public void TestPessoaOK()
        {
            Pessoa pess = new Pessoa() { PessoaId = 1, Nome = "MARCIO NIZZOLA", DataDeNascimento = DateTime.Now.AddYears(-100), Sexo = "M" };
            PessoaCreateValidator validator = new PessoaCreateValidator();

            Assert.True(validator.Validate(pess));
        }

        [Fact(DisplayName = "Classe vazia Inválida")]
        public void TestPessoaFail()
        {
            Pessoa pess = new Pessoa();
            PessoaCreateValidator validator = new PessoaCreateValidator();

            Assert.False(validator.Validate(pess));
        }

        [Theory(DisplayName ="Validar datas")]
        [InlineData( "01/01/2019" )]
        [InlineData( "31/12/1650")]
        [InlineData("27/09/1980")]
        public void TestPessoaDataNascimentoValida( string nasc)
        {
            Pessoa pess = new Pessoa() { PessoaId = 1,
                        Nome = "MARCIO NIZZOLA",
                        DataDeNascimento = DateTime.Parse(nasc),
                        Sexo = "M" };

            PessoaCreateValidator validator = new PessoaCreateValidator();

            Assert.True(validator.Validate(pess));
        }
    }
}
