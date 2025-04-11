using Clientes.Aplicacao.Commands.CreateCliente;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestCliente.Commands
{
    public class CreateClienteCommandValidatorTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly CreateClienteCommandValidator _validator;

        public CreateClienteCommandValidatorTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _validator = new CreateClienteCommandValidator(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task DeveFalhar_QuandoCPF_JaExistir()
        {
            // Arrange
            var cpf = "12345678900";
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComDocumentoAsync(cpf, null))
                .ReturnsAsync(true);

            var command = new CreateClienteCommand
            {
                Nome = "Teste",
                Documento = cpf,
                TipoDocumento = TipoDocumento.CPF,
                DataNascimento = DateTime.Now.AddYears(-20),
                Email = "teste@teste.com",
                Telefone = "9999999999",
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
            result.ShouldHaveValidationErrorFor(c => c.Documento)
                .WithErrorMessage("Já existe um cliente cadastrado com este documento");
        }

        [Fact]
        public async Task DeveFalhar_QuandoEmail_JaExistir()
        {
            // Arrange
            var email = "teste@teste.com";
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComEmailAsync(email, It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var command = new CreateClienteCommand
            {
                Nome = "Teste",
                Documento = "12345678900",
                TipoDocumento = TipoDocumento.CPF,
                DataNascimento = DateTime.Now.AddYears(-20),
                Email = email,
                Telefone = "9999999999",
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
                .WithErrorMessage("Email já está em uso");
        }

        [Fact]
        public async Task DeveFalhar_QuandoPessoaFisica_MenorDe18Anos()
        {
            // Arrange
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComDocumentoAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false);

            var command = new CreateClienteCommand
            {
                Nome = "Teste",
                Documento = "12345678900",
                TipoDocumento = TipoDocumento.CPF,
                DataNascimento = DateTime.Now.AddYears(-17),
                Email = "teste@teste.com",
                Telefone = "9999999999",
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
            result.ShouldHaveValidationErrorFor(c => c.DataNascimento)
                .WithErrorMessage("Cliente deve ter pelo menos 18 anos");
        }

        [Fact]
        public async Task DeveFalhar_QuandoPessoaJuridica_SemInscricaoEstadual_ENaoIsento()
        {
            // Arrange
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComDocumentoAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false);

            var command = new CreateClienteCommand
            {
                Nome = "Empresa Teste",
                Documento = "12345678901234",
                TipoDocumento = TipoDocumento.CNPJ,
                DataNascimento = DateTime.Now.AddYears(-5),
                Email = "empresa@teste.com",
                Telefone = "9999999999",
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Numero = "123",
                Bairro = "Bairro Teste",
                Cidade = "Cidade Teste",
                Estado = "SP",
                InscricaoEstadual = "",
                Isento = false
            };

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.InscricaoEstadual)
                .WithErrorMessage("É necessário informar a Inscrição Estadual ou marcar como Isento");
        }

        [Fact]
        public async Task DevePassar_QuandoPessoaJuridica_ComInscricaoEstadual()
        {
            // Arrange
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComDocumentoAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false);

            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComEmailAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var command = new CreateClienteCommand
            {
                Nome = "Empresa Teste",
                Documento = "12345678901234",
                TipoDocumento = TipoDocumento.CNPJ,
                DataNascimento = DateTime.Now.AddYears(-5),
                Email = "empresa@teste.com",
                Telefone = "9999999999",
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Numero = "123",
                Bairro = "Bairro Teste",
                Cidade = "Cidade Teste",
                Estado = "SP",
                InscricaoEstadual = "123456789",
                Isento = false
            };

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
        }

        [Fact]
        public async Task DevePassar_QuandoPessoaJuridica_Isenta()
        {
            // Arrange
            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComDocumentoAsync(It.IsAny<string>(), null))
                .ReturnsAsync(false);

            _clienteRepositoryMock.Setup(repo =>
                repo.ExisteClienteComEmailAsync(It.IsAny<string>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var command = new CreateClienteCommand
            {
                Nome = "Empresa Teste",
                Documento = "12345678901234",
                TipoDocumento = TipoDocumento.CNPJ,
                DataNascimento = DateTime.Now.AddYears(-5),
                Email = "empresa@teste.com",
                Telefone = "9999999999",
                Cep = "12345678",
                Logradouro = "Rua Teste",
                Numero = "123",
                Bairro = "Bairro Teste",
                Cidade = "Cidade Teste",
                Estado = "SP",
                InscricaoEstadual = "",
                Isento = true
            };

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
        }
    }
}