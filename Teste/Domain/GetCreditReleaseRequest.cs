namespace Teste.Domain
{
    public class GetCreditReleaseRequest
    {
        public decimal Valor { get; set; }
        public string Tipo{ get; set; }
        public int QtyParcelas{ get; set; }
        public DateTime VencimentoInicial{ get; set; }
    }
}
