using LanchesMvc.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMvc.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatorioVendasController : Controller
{
    private readonly RelatorioVendasService _relatorioVendasService;

    public AdminRelatorioVendasController(RelatorioVendasService relatorioVendasService)
    {
        _relatorioVendasService = relatorioVendasService;
    }

    public IActionResult Index() 
        => View();

    public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
            minDate = new DateTime(DateTime.Now.Year, 1, 1);

        if (!maxDate.HasValue)
            maxDate = DateTime.Now;

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await _relatorioVendasService.FindByDataAsync(minDate, maxDate);

        return View(result);
    }
}