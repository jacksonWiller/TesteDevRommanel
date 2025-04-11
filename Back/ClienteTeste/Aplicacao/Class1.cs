using Clientes.Aplicacao.Commands.UpadateCliente;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Dominio.ObjetosDeValor;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestCliente.Commands
{
    public class UpdateClienteCommandTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly UpdateClienteCommandValidator _validator;
        private readonly UpdateClienteCommandHandler _handler;

        public UpdateClienteCommandTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _validator = new UpdateClienteCommandValidator(_clienteRepositoryMock.Object);
            _handler = new UpdateClienteCommandHandler(
                _validator,
                _clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task Validacao_DeveFalhar_QuandoIdade_MenorQue18Anos_ParaPessoaFisica()
        {
            // Arrange
            var command = new UpdateClienteCommand
            {
                Id = Guid.NewGuid(),
                Nome = "Cliente Teste",
                Documento = "52998224725", // CPF válido
                TipoDocumento = TipoDocumento.CPF,
                DataNascimento = DateTime.Now.AddYears(-17).AddDays(-364), // 17 anos
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
            result.ShouldHaveValidationErrorFor(c => c.DataNascimento)
                .WithErrorMessage("Cliente deve ter pelo menos 18 anos");
        }

        //[Fact]
        //public async Task Validacao_DevePassar_QuandoIdade_MaiorQue18Anos_ParaPessoaFisica()
        //{
        //    // Arrange
        //    var command = new UpdateClienteCommand
        //    {
        //        Id = Guid.NewGuid(),
        //        Nome = "Cliente Teste",
        //        Documento = "52998224725", // CPF válido
        //        TipoDocumento = TipoDocumento.CPF,
        //        DataNascimento = DateTime.Now.AddYears(-18).AddDays(-1), // 18 anos e 1 dia
        //        Email = "email@teste.com",
        //        Telefone = "11987654321",
        //        Cep = "12345678",
        //        Logradouro = "Rua Teste",
        //        Numero = "123",
        //        Bairro = "Bairro Teste",
        //        Cidade = "Cidade Teste",
        //        Estado = "SP"
        //    };

        //    // Configurar mock para evitar falha na validação de documento/email
        //    _clienteRepositoryMock.Setup(r => r.GetClienteByIdAsync(command.Id))
        //        .ReturnsAsync(CriarClienteExistente(command.Id));

        //    // Act
        //    var result = await _validator.TestValidateAsync(command);

        //    // Assert
        //    result.ShouldNotHaveValidationErrorFor(c => c.DataNascimento);
        //}

        [Fact]
        public async Task Validacao_DeveFalhar_QuandoPessoaJuridica_SemInscricaoEstadual_NaoIsenta()
        {
            // Arrange
            var command = new UpdateClienteCommand
            {
                Id = Guid.NewGuid(),
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
                Isento = false // Não isento
            };

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.InscricaoEstadual)
                .WithErrorMessage("É necessário informar a Inscrição Estadual ou marcar como Isento");
        }

        [Fact]
        public async Task Validacao_DevePassar_QuandoPessoaJuridica_ComInscricaoEstadual()
        {
            // Arrange
            var command = new UpdateClienteCommand
            {
                Id = Guid.NewGuid(),
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
        public async Task Validacao_DevePassar_QuandoPessoaJuridica_Isenta()
        {
            // Arrange
            var command = new UpdateClienteCommand
            {
                Id = Guid.NewGuid(),
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
                Isento = true // Isenta
            };

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
        }

        //[Fact]
        //public async Task Handler_DeveFalhar_QuandoDocumentoJaExistePraOutroCliente()
        //{
        //    // Arrange
        //    var clienteId = Guid.NewGuid();
        //    var outroClienteId = Guid.NewGuid();
        //    var documento = "77888999000111";

        //    var command = new UpdateClienteCommand
        //    {
        //        Id = clienteId,
        //        Nome = "Empresa Teste",
        //        Documento = documento,
        //        TipoDocumento = TipoDocumento.CNPJ,
        //        DataNascimento = DateTime.Now.AddYears(-5),
        //        Email = "empresa@teste.com",
        //        Telefone = "1133334444",
        //        Cep = "12345678",
        //        Logradouro = "Rua Teste",
        //        Numero = "123",
        //        Bairro = "Bairro Teste",
        //        Cidade = "Cidade Teste",
        //        Estado = "SP",
        //        InscricaoEstadual = "123456789",
        //        Isento = false
        //    };

        //    var clienteExistente = CriarClienteExistente(clienteId);
        //    clienteExistente.Documento = new Documento("documento-antigo", TipoDocumento.CNPJ);

        //    var outroCliente = CriarClienteExistente(outroClienteId);
        //    outroCliente.Documento = new Documento(documento, TipoDocumento.CNPJ);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByIdAsync(clienteId))
        //        .ReturnsAsync(clienteExistente);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByDocumentoAsync(documento))
        //        .ReturnsAsync(outroCliente);

        //    // Act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //    Assert.Contains("Já existe um cliente com este documento", result.Errors);
        //}

        //[Fact]
        //public async Task Handler_DeveFalhar_QuandoEmailJaExistePraOutroCliente()
        //{
        //    // Arrange
        //    var clienteId = Guid.NewGuid();
        //    var outroClienteId = Guid.NewGuid();
        //    var email = "empresa@teste.com";

        //    var command = new UpdateClienteCommand
        //    {
        //        Id = clienteId,
        //        Nome = "Empresa Teste",
        //        Documento = "77888999000111",
        //        TipoDocumento = TipoDocumento.CNPJ,
        //        DataNascimento = DateTime.Now.AddYears(-5),
        //        Email = email,
        //        Telefone = "1133334444",
        //        Cep = "12345678",
        //        Logradouro = "Rua Teste",
        //        Numero = "123",
        //        Bairro = "Bairro Teste",
        //        Cidade = "Cidade Teste",
        //        Estado = "SP",
        //        InscricaoEstadual = "123456789",
        //        Isento = false
        //    };

        //    var clienteExistente = CriarClienteExistente(clienteId);
        //    clienteExistente.Email = new Email("email-antigo@teste.com");

        //    var outroCliente = CriarClienteExistente(outroClienteId);
        //    outroCliente.Email = new Email(email);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByIdAsync(clienteId))
        //        .ReturnsAsync(clienteExistente);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByDocumentoAsync(It.IsAny<string>()))
        //        .ReturnsAsync((Cliente)null);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByEmailAsync(email))
        //        .ReturnsAsync(outroCliente);

        //    // Act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //    Assert.Contains("Já existe um cliente com este e-mail", result.Errors);
        //}

        //[Fact]
        //public async Task Handler_DeveAtualizarCliente_QuandoDadosValidos()
        //{
        //    // Arrange
        //    var clienteId = Guid.NewGuid();
        //    var command = new UpdateClienteCommand
        //    {
        //        Id = clienteId,
        //        Nome = "Empresa Teste Atualizada",
        //        Documento = "77888999000111",
        //        TipoDocumento = TipoDocumento.CNPJ,
        //        DataNascimento = DateTime.Now.AddYears(-5),
        //        Email = "empresa@teste.com",
        //        Telefone = "1133334444",
        //        Cep = "12345678",
        //        Logradouro = "Rua Teste",
        //        Numero = "123",
        //        Bairro = "Bairro Teste",
        //        Cidade = "Cidade Teste",
        //        Estado = "SP",
        //        InscricaoEstadual = "123456789",
        //        Isento = false
        //    };

        //    var clienteExistente = CriarClienteExistente(clienteId);
        //    clienteExistente.Documento = new Documento(command.Documento, command.TipoDocumento);
        //    clienteExistente.Email = new Email(command.Email);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByIdAsync(clienteId))
        //        .ReturnsAsync(clienteExistente);

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByDocumentoAsync(command.Documento))
        //        .ReturnsAsync(clienteExistente); // Mesmo cliente, então é ok

        //    _clienteRepositoryMock.Setup(r => r.GetClienteByEmailAsync(command.Email))
        //        .ReturnsAsync(clienteExistente); // Mesmo cliente, então é ok

        //    // Act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    _clienteRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Cliente>()), Times.Once);
        //}

        //private Cliente CriarClienteExistente(Guid id)
        //{
        //    var documento = new Documento("12345678900", TipoDocumento.CPF);
        //    var telefone = new Telefone("11987654321");
        //    var email = new Email("email@teste.com");
        //    var endereco = new Endereco("12345678", "Rua Teste", "123", "Bairro Teste", "Cidade Teste", "SP");

        //    return new Cliente(
        //        id,
        //        "Cliente Teste",
        //        documento,
        //        DateTime.Now.AddYears(-20),
        //        telefone,
        //        email,
        //        endereco,
        //        string.Empty,
        //        true,
        //        false
        //    );
        //}
    }
}