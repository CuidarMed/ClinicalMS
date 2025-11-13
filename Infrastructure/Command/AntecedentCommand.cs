using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;

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

    public async Task<Antedecent> updateAntecedent(int id, AntecedentUpdate antecedentUpd)
    {
        var antecedent = new Antedecent
        {
            AntedecentId = id,
            Category = antecedentUpd.Category,
            Description = antecedentUpd.Description,
            StartDate = antecedentUpd.StartDate,
            EndTime = antecedentUpd.EndDate,
            Status = antecedentUpd.Status,
            UpdatedAt = DateTime.Now
        };

        context.Antedecents.Attach(antecedent);
        context.Entry(antecedent).Property(a => a.Category).IsModified = true;
        context.Entry(antecedent).Property(a => a.Description).IsModified = true;
        context.Entry(antecedent).Property(a => a.StartDate).IsModified = true;
        context.Entry(antecedent).Property(a => a.EndTime).IsModified = true;
        context.Entry(antecedent).Property(a => a.Status).IsModified = true;
        context.Entry(antecedent).Property(a => a.UpdatedAt).IsModified = true;

        await context.SaveChangesAsync();

        return antecedent;
    }
}
