namespace Clientes.Dominio.ObjetosDeValor
{
    public class Telefone
    {
        public string Numero { get; private set; }

        protected Telefone() { }

        public Telefone(string numero)
        {
            Numero = numero;
        }

        public override string ToString()
        {
            return $"{Numero}";
        }
    }
}
