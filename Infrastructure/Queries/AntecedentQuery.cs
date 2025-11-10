using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

public class AntecedentQuery :IAntecedentQuery
{
    private readonly AppDbContext context;

    public AntecedentQuery(AppDbContext context)
	{
        this.context = context;
    }

    public async Task<Antedecent?> GetByIdAsync(int id)
    {
        var antecedent= await context.Antedecents
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AntedecentId == id);
        return antecedent;
    }
}
