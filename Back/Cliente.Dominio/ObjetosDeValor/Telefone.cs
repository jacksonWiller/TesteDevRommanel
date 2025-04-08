namespace Clientes.Dominio.ObjetosDeValor
{
    public class Telefone
    {
        public string DDD { get; private set; }
        public string Numero { get; private set; }

        protected Telefone() { }

        public Telefone(string ddd, string numero)
        {
            DDD = ddd;
            Numero = numero;
        }

        public override string ToString()
        {
            return $"({DDD}) {Numero}";
        }
    }
}
