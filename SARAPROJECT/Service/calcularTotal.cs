using SARAPROJECT.Models;

namespace SARAPROJECT.Service
{
    public class calcularTotal
    {
        public decimal retornarTotal(List<DetPago> detPagoList)
        {
            decimal total = 0;
            foreach(var item in detPagoList)
            {
                total += item.Monto; 
            }
            return total;
        }
        public decimal calcularCambio(decimal Total, decimal Pagado)
        {
            decimal efectivo = Pagado - Total;
            if (efectivo < 0)
                return 0;
            return Pagado-Total; 
        }
    }
}
