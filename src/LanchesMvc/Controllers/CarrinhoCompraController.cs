using LanchesMvc.Models;
using LanchesMvc.Repositories.Interfaces;
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
            return View();
        }
    }
}