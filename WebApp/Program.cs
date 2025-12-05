using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebApp;
using WebApp.Clients;
using WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddScoped<ITokenStorage,SessionTokenStorage>();

builder.Services.AddTransient<AuthHttpClientHeader>();

builder.Services.AddHttpClient("ApiClient", cl =>
{
	cl.BaseAddress = new Uri("http://localhost:5117/"); //TOREMEMBER: This must be change to Api current port.
}).AddHttpMessageHandler<AuthHttpClientHeader>();

builder.Services.AddScoped<AuthClient>();

await builder.Build().RunAsync();
