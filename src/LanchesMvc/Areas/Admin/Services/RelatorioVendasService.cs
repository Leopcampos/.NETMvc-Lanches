﻿using LanchesMvc.Context;
using LanchesMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMvc.Areas.Admin.Services;

public class RelatorioVendasService
{
    private readonly AppDbContext _context;

    public RelatorioVendasService(AppDbContext context)
        => _context = context;

    public async Task<List<Pedido>> FindByDataAsync(DateTime? minDate, DateTime? maxDate)
    {
        var resultado = from obj in _context.Pedidos select obj;

        if (minDate.HasValue)
            resultado = resultado.Where(p => p.PedidoEnviado >= minDate.Value);

        if (maxDate.HasValue)
            resultado = resultado.Where(p => p.PedidoEnviado <= maxDate.Value);

        return await resultado
                        .Include(p => p.PedidoItens)
                        .ThenInclude(pd => pd.Lanche)
                        .OrderByDescending(p => p.PedidoEnviado)
                        .ToListAsync();
    }
}