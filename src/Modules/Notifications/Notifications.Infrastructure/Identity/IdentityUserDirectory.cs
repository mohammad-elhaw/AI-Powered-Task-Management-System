using Notifications.Application.Abstractions;
using System.Net.Http.Json;

namespace Notifications.Infrastructure.Identity;

public sealed class IdentityUserDirectory(
    HttpClient httpClient)
    : IUserDirectory
{
    public async Task<UserContact?> GetUserContact(Guid userId)
    {
        //var user = await httpClient.GetFromJsonAsync<IdentityUserResponse>(
        //    $"api/users/{userId}");
        
        using var response = await httpClient.GetAsync($"api/users/{userId}");
        
        if (!response.IsSuccessStatusCode) return null;
        
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<IdentityUserResponse>>();

        var user = apiResponse?.Data;

        return user is null
            ? null
            : new UserContact
            (
                UserId: user.Id,
                Email: user.Email
            );
    }
}
