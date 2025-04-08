using Clientes.Dominio.ObjetosDeValor;

namespace Clientes.Dominio.Entidade
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Documento Documento { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Telefone Telefone { get; private set; }
        public Email Email { get; private set; }
        public Endereco Endereco { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public bool Isento { get; private set; }

        protected Cliente() { }

        public Cliente(string nome, Documento documento, DateTime dataNascimento,
                      Telefone telefone, Email email, Endereco endereco,
                      string inscricaoEstadual = null, bool isento = false)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Endereco = endereco;
            InscricaoEstadual = inscricaoEstadual;
            Isento = isento;
        }

        public void AtualizarDados(string nome, DateTime dataNascimento,
                                 Telefone telefone, Endereco endereco,
                                 string inscricaoEstadual = null, bool isento = false)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Endereco = endereco;
            InscricaoEstadual = inscricaoEstadual;
            Isento = isento;
        }

    }
}
