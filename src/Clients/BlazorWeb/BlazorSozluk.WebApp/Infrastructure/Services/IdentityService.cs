using Blazored.LocalStorage;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using BlazorSozluk.WebApp.Infrastructure.Extensions;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient client;
    private readonly ISyncLocalStorageService syncLocalStorageService;

    public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
    {
        this.client = client;
        this.syncLocalStorageService = syncLocalStorageService;
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

    public string GetUserToken()
    {
        return syncLocalStorageService.GetToken();
    }

    public string GetUserName()
    {
        return syncLocalStorageService.GetUserName();
    }

    public Guid GetUserId()
    {
        return syncLocalStorageService.GetUserId();
    }

    public async Task<bool> Login(LoginUserCommand command)
    {
        var httpResponse = await client.PostAsJsonAsync("/api/User/Login", command);
        var responseStr = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                responseStr = validation.FlattenErrors;
                throw new DatabaseValidationException(responseStr);
            }

            return false;
        }

        var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);
        if (!string.IsNullOrEmpty(response.Token))//Login Success
        {
            syncLocalStorageService.SetToken(response.Token);
            syncLocalStorageService.SetUserName(response.UserName);
            syncLocalStorageService.SetUserId(response.Id);

            //TODO Check after auth
            //((AuthStateProvider)authStateProvider).NotifyUserLogin(response.UserName, response.Id);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.Token);

            return true;
        }

        return false;
    }

    public void logout()
    {
        syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
        syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
        syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

        //TODO Check after auth
        //((AuthStateProvider)authStateProvider).NotifyUserLogin(response.UserName, response.Id);

        client.DefaultRequestHeaders.Authorization = null;
    }
}
