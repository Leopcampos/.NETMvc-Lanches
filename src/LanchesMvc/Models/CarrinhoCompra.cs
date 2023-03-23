using LanchesMvc.Data;
using Microsoft.Extensions.WebEncoders.Testing;

namespace LanchesMvc.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //Define uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Obtém um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //Obtém ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //Atribui o Id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(context) 
            { 
                CarrinhoCompraId = carrinhoId 
            };
        }
    }
}