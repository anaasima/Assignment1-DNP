using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using FINALAssignment.Data;
using FINALAssignment.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace FINALAssignment.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
private readonly IJSRuntime jsRuntime;
    private readonly IUserService userService;

    private User cachedUser;

    public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService) {
        this.jsRuntime = jsRuntime;
        this.userService = userService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        var identity = new ClaimsIdentity();
        if (cachedUser == null) {
            string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            if (!string.IsNullOrEmpty(userAsJson)) {
                cachedUser = JsonSerializer.Deserialize<User>(userAsJson);

                identity = SetupClaimsForUser(cachedUser);
            }
        } else {
            identity = SetupClaimsForUser(cachedUser);
        }

        ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
    }

    public void ValidateLogin(string username, string password) {
        Console.WriteLine("Validating log in");
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

        ClaimsIdentity identity = new ClaimsIdentity();
        try {
            User user = userService.ValidateUser(username, password);
            identity = SetupClaimsForUser(user);
            string serialisedData = JsonSerializer.Serialize(user);
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
            cachedUser = user;
        } catch (Exception e) {
            throw e;
        }

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
    }

    public void CreateUser(string name, string username, string password, int id)
    {
        Console.WriteLine("Creating new user");
        if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
        if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");
        if (string.IsNullOrEmpty(name)) throw new Exception("Enter name");
        try {
            User user = userService.CreateUser(name, username, password, id);
            string serialisedData = JsonSerializer.Serialize(user);
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
            cachedUser = user;
        } catch (Exception e) {
            throw e;
        }
    }
    public void Logout() {
        cachedUser = null;
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private ClaimsIdentity SetupClaimsForUser(User user) {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim("Role", user.Role));
        claims.Add(new Claim("City", user.City));
        claims.Add(new Claim("Domain", user.Domain));
        claims.Add(new Claim("BirthYear", user.BirthYear.ToString()));
        claims.Add(new Claim("Level", user.SecurityLevel.ToString()));
        claims.Add(new Claim("Name", user.Name));
        claims.Add(new Claim("ID", user.ID.ToString()));


        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
        return identity;
    }
}
}