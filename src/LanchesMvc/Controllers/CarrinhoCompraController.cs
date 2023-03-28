using LanchesMvc.Models;
using LanchesMvc.Repositories.Interfaces;
using LanchesMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMvc.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        //Para adicionar a instancia de CarrinhoCompra carrinhoCompram foi necessário adicionar AddScooped no Program.cs
        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }

        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            var lanche = _lancheRepository.GetLancheById(lancheId);

            if (lanche != null)
                _carrinhoCompra.RemoverDoCarrinho(lanche);

            return RedirectToAction("Index");
        }
    }
}