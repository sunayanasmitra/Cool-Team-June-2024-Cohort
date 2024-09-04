using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HealthCareApp.Client;
using MudBlazor.Services;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("HealthCareApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
	.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PublicHttp>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HealthCareApp.ServerAPI"));
//builder.Services.TryAddEnumerable(
//			  ServiceDescriptor.Singleton<
//				  IPostConfigureOptions<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>>,
//				  ApiAuthorizationOptionsConfiguration>());

// Register FluentValidation services


builder.Services.AddApiAuthorization();
builder.Services.AddMudServices();
await builder.Build().RunAsync();
