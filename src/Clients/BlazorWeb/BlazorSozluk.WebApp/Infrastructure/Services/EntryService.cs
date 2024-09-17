using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient client;

    public EntryService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var response = await client.GetFromJsonAsync<List<GetEntriesViewModel>>($"/api/Entry?TodaysEntries=false&count=30");

        return response;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var response = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/Entry/{entryId}");

        return response;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/Entry/MainPageEntries?page={page}&pageSize={pageSize}");

        return response;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string username = null)
    {
        var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/Entry/UserEntries?userName={username}&page={page}&pageSize={pageSize}");

        return response;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var response = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/Entry/Comments/{entryId}?page={page}&pageSize={pageSize}");

        return response;
    }

    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var response = await client.PostAsJsonAsync($"/api/Entry/CreateEntry", command);

        if (!response.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await response.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var response = await client.PostAsJsonAsync($"/api/Entry/CreateEntryComment", command);

        if (!response.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await response.Content.ReadAsStringAsync();

        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
    {
        var response = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/Entry/Search?searchText={searchText}");

        return response;
    }
}

