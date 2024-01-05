using LanchesMvc.Models;
using LanchesMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMvc.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //Obtém os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            //Verifica se existem itens de pedido
            if(_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lance...");
            }

            //calcula o total de itens e o total de pedido
            foreach (var item in itens)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //Atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //Valida os dados do pedido
            if (ModelState.IsValid)
            {
                //Cria o pedido e os detalhes
                _pedidoRepository.CriarPedido(pedido);

                //Define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //Limpar o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //Exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}