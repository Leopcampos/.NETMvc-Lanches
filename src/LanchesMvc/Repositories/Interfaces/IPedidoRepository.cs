using LanchesMvc.Models;

namespace LanchesMvc.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}