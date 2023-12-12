using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
        services.AddDbContext<BlazorSozlukContext>(conf =>
        {
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        var SeedDataActive = configuration["SeedDataActive"].ToString() == "True";
        if(SeedDataActive)
        {
            var context = services.BuildServiceProvider().GetRequiredService<BlazorSozlukContext>();

            var seedData = new SeedData();
            seedData.SeedAsync(context).GetAwaiter().GetResult();
        }

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
        services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
        services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();
        services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();

        return services;
    }
}
