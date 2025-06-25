using Microsoft.Extensions.DependencyInjection;
using TDDPalestraXUnitExample3.ErroHelpers;
using TDDPalestraXUnitExample3.Extension;
using TDDPalestraXUnitExample3.Model.Enum;
using TDDPalestraXUnitExample3.Validator;
using Xunit;
using XUnitPalestraTDDExample3.Builder;

namespace XUnitPalestraTDDExample3;

public class UserModelValidatorTest
{
    private readonly UserModelValidator _validator;
    private readonly UserModelBuilder _builder;
    public UserModelValidatorTest()
    {
        var provider = new ServiceCollection().AddScoped<UserModelValidator>().BuildServiceProvider();

        _validator = provider.GetService<UserModelValidator>();

        _builder = new UserModelBuilder();
    }

    [Fact(DisplayName = "Should be a valid state")]
    public async Task ShouldBeAValidState()
    {
        var instance = _builder.Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Fact(DisplayName = "Should be a valid Gender")]
    public async Task ShouldBeAValidGender()
    {
        var instance = _builder.With(a => a.Gender = GenderEnum.Uninformed).Build();

        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Theory(DisplayName = "Should have a valid Password length")]
    [InlineData(1)]
    [InlineData(200)]
    public async Task ShouldHaveAValidPasswordLength(int length)
    {
        var instance = _builder.With(x => x.Password = string.Concat(Enumerable.Repeat("0", length))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Theory(DisplayName = "Should have a valid Login length")]
    [InlineData(1)]
    [InlineData(50)]
    public async Task ShouldHaveAValidLoginLength(int length)
    {
        var instance = _builder.With(x => x.Login = string.Concat(Enumerable.Repeat("0", length))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Theory(DisplayName = "Should have a valid Name length")]
    [InlineData(1)]
    [InlineData(100)]
    public async Task ShouldHaveAValidNameLength(int length)
    {
        var instance = _builder.With(x => x.Name = string.Concat(Enumerable.Repeat("0", length))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Fact(DisplayName = "Should have an invalid Password length")]
    public async Task ShouldHaveAnInvalidPasswordLength()
    {
        var instance = _builder.With(x => x.Password = string.Concat(Enumerable.Repeat("0", 201))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_PasswordExceededMaxLength.Description(200));
    }

    [Fact(DisplayName = "Should have an invalid Login length")]
    public async Task ShouldHaveAnInvalidLoginLength()
    {
        var instance = _builder.With(x => x.Login = string.Concat(Enumerable.Repeat("0", 51))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_LoginExceededMaxLength.Description(50));
    }

    [Fact(DisplayName = "Should have an invalid Name length")]
    public async Task ShouldHaveAnInvalidNameLength()
    {
        var instance = _builder.With(x => x.Name = string.Concat(Enumerable.Repeat("0", 101))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_NameExceededMaxLength.Description(100));
    }

    [Fact(DisplayName = "BirthDate should be mandatory")]
    public async Task ShouldHaveBirthDateFieldFilledUp()
    {
        var instance = _builder.With(x => x.BirthDate = default(DateTime)).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_BirthDateIsMandatory.Description());
    }

    [Fact(DisplayName = "BirthDate should not be a future date")]
    public async Task ShouldNotBeBirthDateFutureDate()
    {
        var instance = _builder.With(x => x.BirthDate = DateTime.Now.AddDays(1)).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_BirthDateCannotBeFutureDate.Description());
    }

    [Fact(DisplayName = "BirthDate should not be a long past date")]
    public async Task ShouldNotBeBirthDateLongPastDate()
    {
        var instance = _builder.With(x => x.BirthDate = new DateTime(1900, 01, 01).AddDays(-1)).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_BirthDateCannotBeLongPast.Description());
    }

    [Theory(DisplayName = "Password should be mandatory")]
    [InlineData(null)]
    [InlineData("")]
    public async Task ShouldHavePasswordFieldFilledUp(string value)
    {
        var instance = _builder.With(x => x.Password = value).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_PasswordIsMandatory.Description());
    }

    [Theory(DisplayName = "Login should be mandatory")]
    [InlineData(null)]
    [InlineData("")]
    public async Task ShouldHaveLoginFieldFilledUp(string value)
    {
        var instance = _builder.With(x => x.Login = value).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_LoginIsMandatory.Description());
    }

    [Theory(DisplayName = "ReferenceId length should be valid")]
    [InlineData(1)]
    [InlineData(50)]
    public async Task ShouldHaveValidReferenceIdLength(int length)
    {
        var instance = _builder.With(x => x.ReferenceId = string.Concat(Enumerable.Repeat("0", length))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Fact(DisplayName = "ReferenceId max length should be invalid")]
    public async Task ShouldHaveInvalidReferenceIdLength()
    {
        var instance = _builder.With(x => x.ReferenceId = string.Concat(Enumerable.Repeat("0", 51))).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_ReferenceIdExceededMaxLength.Description(50));
    }

    [Theory(DisplayName = "Name should be mandatory")]
    [InlineData(null)]
    [InlineData("")]
    public async Task ShouldHaveNameFieldFilledUp(string value)
    {
        var instance = _builder.With(x => x.Name = value).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_NameIsMandatory.Description());
    }

    [Fact(DisplayName = "DocumentCPF should be invalid")]
    public async Task ShouldHaveDocumentCPFField()
    {
        var instance = _builder.With(x => x.DocumentCPF = "KDJSHFKSJDODSIAJDOASIJDASLKJADS").Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_DocumentCPFIsInvalid.Description());
    }

    [Fact(DisplayName = "DocumentCPF should be invalid without format")]
    public async Task ShouldHaveFormatedDocumentCPFField()
    {
        var instance = _builder.With(x => x.DocumentCPF = "00000000000").Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_DocumentCPFIsInvalid.Description());
    }

    [Fact(DisplayName = "DocumentRG should be invalid")]
    public async Task ShouldHaveInvalidDocumentRGField()
    {
        var instance = _builder.With(x => x.DocumentRG = "KDJSHFKSJDODSIAJDOASIJDASLKJADS").Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_DocumentRGIsInvalid.Description());
    }

    [Theory(DisplayName = "CellPhoneNumber should be invalid")]
    [InlineData("LASKDJASLALSKDJ")]
    [InlineData("(11) 4011-252F")]
    [InlineData("(11) 4011-252")]
    [InlineData("(11) 4011-22522")]
    [InlineData("(11) A4011-2252")]
    [InlineData("(11) 4011-2252")]
    [InlineData("(11) 14011-2252")]
    [InlineData("(00) 94011-2252")]
    [InlineData("(1X) 94011-2252")]
    [InlineData("0000000")]
    [InlineData("a00000000")]
    [InlineData("-14129582")]
    [InlineData("11998540515")]
    [InlineData("(A1)99854-0513")]
    public async Task ShouldHaveInvalidCellPhoneNumberField(string phoneNumber)
    {
        var instance = _builder.With(x => x.CellPhoneNumber = phoneNumber).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.False(validationResult.IsValid);
        Assert.Contains(validationResult.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_CellphoneNumberIsInvalid.Description());
    }

    [Theory(DisplayName = "CellPhoneNumber should be valid")]
    [InlineData("(11) 99855-0515")]
    [InlineData("(21) 99855-0515")]
    [InlineData("(19) 94855-0515")]
    public async Task ShouldHaveValidCellPhoneNumberField(string phoneNumber)
    {
        var instance = _builder.With(x => x.CellPhoneNumber = phoneNumber).Build();
        var validationResult = await _validator.ValidateAsync(instance);
        Assert.True(validationResult.IsValid);
    }

    [Theory(DisplayName = "Email should be invalid")]
    [InlineData("a@")]
    [InlineData("a@a")]
    [InlineData("a@a.a")]
    [InlineData("_@_.com")]
    public async Task ShouldNotHaveInvalidEmail(string email)
    {
        var result = await _validator.ValidateAsync(_builder.With(w => w.Email = email).Build());
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.ToMetaError().Error.Message == AccountBusinessError.Account_EmailIsInvalid.Description());
    }
}
