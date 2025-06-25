using TDDExemplo1.Models;
using TDDExemplo1.Validator;
using Xunit;

namespace XUnitTestProject;

public class PessoaUnitTest
{
    [Fact(DisplayName = "Classe completa v�lida")]
    public void TestPessoaOK()
    {
        Pessoa pess = new Pessoa() { PessoaId = 1, Nome = "MARCIO NIZZOLA", DataDeNascimento = DateTime.Now.AddYears(-100), Sexo = "M" };
        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.True(validator.Validate(pess));
    }

    [Fact(DisplayName = "Classe vazia Inv�lida")]
    public void TestPessoaFail()
    {
        Pessoa pess = new Pessoa();
        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.False(validator.Validate(pess));
    }

    [Theory(DisplayName = "Validar datas inv�lidas")]
    [InlineData(null)]
    [InlineData("1650-12-31")]
    public void TestPessoaDataNascimentoInValida(string nasc)
    {
        DateTime dataNascimento = nasc == null ? DateTime.MinValue : DateTime.Parse(nasc);
        Pessoa pess = new Pessoa()
        {
            PessoaId = 1,
            Nome = "MARCIO NIZZOLA",
            DataDeNascimento = dataNascimento,
            Sexo = "M"
        };

        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.False(validator.Validate(pess));
    }

    [Theory(DisplayName = "Validar datas v�lidas")]
    [InlineData("2019-01-01")]
    [InlineData("1980-09-27")]
    public void TestPessoaDataNascimentoValida(string nasc)
    {
        Pessoa pess = new Pessoa()
        {
            PessoaId = 1,
            Nome = "MARCIO NIZZOLA",
            DataDeNascimento = DateTime.Parse(nasc),
            Sexo = "M"
        };

        PessoaCreateValidator validator = new PessoaCreateValidator();

        Assert.True(validator.Validate(pess));
    }
}
