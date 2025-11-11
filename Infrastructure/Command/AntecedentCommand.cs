using System;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

public class AntecedentCommand : IAntecedentCommand
{
    private readonly AppDbContext context;

    public AntecedentCommand(AppDbContext context)
	{
        this.context = context;
    }

    public async Task<int> DeleteAsync(Antedecent antedecent)
    {
        context.Antedecents.Update(antedecent);
        await context.SaveChangesAsync();
        return antedecent.AntedecentId;
    }
}
