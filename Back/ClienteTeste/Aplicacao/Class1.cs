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

            var result = await _validator.TestValidateAsync(command);

            result.ShouldHaveValidationErrorFor(c => c.DataNascimento)
                .WithErrorMessage("Cliente deve ter pelo menos 18 anos");
        }

        [Fact]
        public async Task Validacao_DeveFalhar_QuandoPessoaJuridica_SemInscricaoEstadual_NaoIsenta()
        {
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

            var result = await _validator.TestValidateAsync(command);

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

            var result = await _validator.TestValidateAsync(command);

            result.ShouldNotHaveValidationErrorFor(c => c.InscricaoEstadual);
        }

        [Fact]
        public async Task Validacao_DevePassar_QuandoPessoaJuridica_Isenta()
        {
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
    }
}