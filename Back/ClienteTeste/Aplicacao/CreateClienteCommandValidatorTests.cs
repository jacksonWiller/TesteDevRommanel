using Clientes.Aplicacao.Commands.CreateCliente;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestCliente.Commands;

public class EmailUnicoValidationTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly CreateClienteCommandValidator _validator;

    public EmailUnicoValidationTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _validator = new CreateClienteCommandValidator(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task DeveFalhar_QuandoEmail_JaExistir()
    {
        // Arrange
        var email = "email.existente@teste.com";
        _clienteRepositoryMock.Setup(repo =>
            repo.ExisteClienteComEmailAsync(email))
            .ReturnsAsync(true);

        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725", // CPF válido
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = email,
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage("Este e-mail já está em uso");
    }

    [Fact]
    public async Task DevePassar_QuandoEmail_NaoExistir()
    {
        // Arrange
        var email = "email.novo@teste.com";
        _clienteRepositoryMock.Setup(repo =>
            repo.ExisteClienteComEmailAsync(email))
            .ReturnsAsync(false);

        _clienteRepositoryMock.Setup(repo =>
            repo.ExisteClienteComDocumentoAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725", // CPF válido
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = email,
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public async Task DeveFalhar_QuandoEmail_FormatoInvalido()
    {
        // Arrange
        var email = "email-invalido";

        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725", // CPF válido
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = email,
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage("E-mail em formato inválido");
    }

    [Fact]
    public async Task DeveFalhar_QuandoEmail_Vazio()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725",
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "",
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage("O e-mail é obrigatório");
    }


}

public class InscricaoEstadualPessoaJuridicaTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly CreateClienteCommandValidator _validator;

    public InscricaoEstadualPessoaJuridicaTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _validator = new CreateClienteCommandValidator(_clienteRepositoryMock.Object);

        // Configurar o mock para não falhar por causa de outras regras
        _clienteRepositoryMock.Setup(repo =>
            repo.ExisteClienteComDocumentoAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        _clienteRepositoryMock.Setup(repo =>
            repo.ExisteClienteComEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
    }

    [Fact]
    public async Task DeveFalhar_Quando_PessoaJuridica_SemIE_NaoIsenta()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Empresa Teste",
            Documento = "77888999000111", // CNPJ válido
            TipoDocumento = TipoDocumento.CNPJ,
            DataNascimento = DateTime.Now.AddYears(-5),
            Email = "empresa@teste.com",
            Telefone = "1133334444",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP",
            InscricaoEstadual = "", // Sem IE
            Isento = false // Não é isento
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.InscricaoEstadual)
            .WithErrorMessage("É necessário informar a Inscrição Estadual ou marcar como Isento");
    }

    [Fact]
    public async Task DevePassar_Quando_PessoaJuridica_ComIE()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Empresa Teste",
            Documento = "77888999000111", // CNPJ válido
            TipoDocumento = TipoDocumento.CNPJ,
            DataNascimento = DateTime.Now.AddYears(-5),
            Email = "empresa@teste.com",
            Telefone = "1133334444",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP",
            InscricaoEstadual = "123456789", // Com IE
            Isento = false
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
    }

    [Fact]
    public async Task DevePassar_Quando_PessoaJuridica_Isenta()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Empresa Teste",
            Documento = "77888999000111", // CNPJ válido
            TipoDocumento = TipoDocumento.CNPJ,
            DataNascimento = DateTime.Now.AddYears(-5),
            Email = "empresa@teste.com",
            Telefone = "1133334444",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP",
            InscricaoEstadual = "", // Sem IE
            Isento = true // Marcado como isento
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
    }

    [Fact]
    public async Task NaoDeveAplicarValidacaoIE_Quando_PessoaFisica()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725", // CPF válido
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "email@teste.com",
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP",
            InscricaoEstadual = "", // Sem IE
            Isento = false // Não isento
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
    }
}

public class CamposObrigatoriosValidationTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly CreateClienteCommandValidator _validator;

    public CamposObrigatoriosValidationTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _validator = new CreateClienteCommandValidator(_clienteRepositoryMock.Object);
    }

    [Theory]
    [InlineData("", "Nome")]
    [InlineData("A", "Nome")]  // Menos de 2 caracteres
    public async Task DeveFalhar_QuandoNome_Invalido(string nome, string campo)
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = nome,
            Documento = "52998224725",
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "email@teste.com",
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Nome);
    }

    [Theory]
    [InlineData("", "Telefone")]
    [InlineData("123", "Telefone")]  // Menos de 10 dígitos
    public async Task DeveFalhar_QuandoTelefone_Invalido(string telefone, string campo)
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725",
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "email@teste.com",
            Telefone = telefone,
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Telefone);
    }

    [Theory]
    [InlineData("", "Cep")]
    [InlineData("1234567", "Cep")]  // Menos de 8 dígitos
    public async Task DeveFalhar_QuandoCep_Invalido(string cep, string campo)
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725",
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "email@teste.com",
            Telefone = "11987654321",
            Cep = cep,
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SP"
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Cep);
    }

    [Fact]
    public async Task DeveFalhar_QuandoEstado_ComMaisDeDoisCaracteres()
    {
        // Arrange
        var command = new CreateClienteCommand
        {
            Nome = "Cliente Teste",
            Documento = "52998224725",
            TipoDocumento = TipoDocumento.CPF,
            DataNascimento = DateTime.Now.AddYears(-20),
            Email = "email@teste.com",
            Telefone = "11987654321",
            Cep = "12345678",
            Logradouro = "Rua Teste",
            Numero = "123",
            Bairro = "Bairro Teste",
            Cidade = "Cidade Teste",
            Estado = "SPE" // Mais de 2 caracteres
        };

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Estado)
            .WithErrorMessage("O estado deve ter 2 caracteres");
    }
}

