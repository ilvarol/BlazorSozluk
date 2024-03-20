using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<PagedViewModel<GetEntriesViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
        Task<List<GetEntriesViewModel>> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntriesViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<PagedViewModel<GetEntriesViewModel>> GetProfilePageEntries(int page, int pageSize, string username = null);
        Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
    }
}