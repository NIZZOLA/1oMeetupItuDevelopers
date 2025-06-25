using System;
using TDDExemplo1.Models;
using TDDExemplo1.Validator;
using Xunit;

namespace XUnitTestProject;

public class PessoaUnitTest
{
    [Fact( DisplayName = "Classe completa v�lida")]
    public void TestPessoaOK()
    {
        Pessoa pess = new Pessoa() 
        { 
            PessoaId = 1,
            Nome = "MARCIO NIZZOLA",
            DataDeNascimento = DateTime.Now.AddYears(-120),
            Sexo = "M" 
        };
        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.True(validator.Validate(pess));
    }

    [Fact(DisplayName = "Classe Inv�lida")]
    public void TestPessoaFail()
    {
        Pessoa pess = new Pessoa();
        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.False(validator.Validate(pess));
    }
}
