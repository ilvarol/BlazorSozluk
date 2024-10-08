﻿using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient client;

    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
    {
        var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

        return userDetail;
    }

    public async Task<UserDetailViewModel> GetUserDetail(string userName)
    {
        var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");

        return userDetail;
    }

    public async Task<bool> UpdateUser(UserDetailViewModel user)
    {
        var res = await client.PostAsJsonAsync<UserDetailViewModel>($"/api/user/update", user);

        return res.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
    {
        var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
        var res = await client.PostAsJsonAsync($"/api/user/ChangePassword", command);

        if (res != null && !res.IsSuccessStatusCode)
        {
            if (res.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var responseStr = await res.Content.ReadAsStringAsync();
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                responseStr = validation.FlattenErrors;
                throw new DatabaseValidationException(responseStr);
            }

            return false;
        }
        return res.IsSuccessStatusCode;
    }
}

