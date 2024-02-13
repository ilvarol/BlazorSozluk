using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.SearchBySubject;

public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IEntryRepository entryRepository;

    public SearchEntryQueryHandler(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }

    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(request.SearchText))
            throw new ArgumentNullException(nameof(request.SearchText));
        else if (request.SearchText.Length < 3)
            throw new ArgumentException(nameof(request.SearchText));

        var result = entryRepository
            .Get(i => EF.Functions.Like(i.Subject, $"{request.SearchText}%"))
            .Select(i => new SearchEntryViewModel
            {
                Id = i.Id,
                Subject = i.Subject
            });

        return await result.ToListAsync(cancellationToken);
    }
}