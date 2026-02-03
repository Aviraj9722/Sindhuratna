using Microsoft.AspNetCore.HttpOverrides;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//
// ?? REQUIRED: Forwarded headers (HTTPS behind proxy)
//
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto;

    // Optional but recommended if behind proxy/load balancer
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

//
// ?? REQUIRED: Cookie settings for PKCE
//
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

//
// ?? MUST be BEFORE Umbraco middleware
//
app.UseForwardedHeaders();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

app.MapControllerRoute(
    name: "districts",
    pattern: "home/index",
    defaults: new { controller = "Home", action = "Index" }
);

app.MapControllerRoute(
    name: "sindhudurg",
    pattern: "district-page",
    defaults: new { controller = "Sindhudurg", action = "Index" }
);

app.MapControllerRoute(
    name: "places",
    pattern: "place-page",
    defaults: new { controller = "Places", action = "Index" }
);
app.MapControllerRoute(
    name: "hotels",
    pattern: "hotel-page",
    defaults: new { controller = "Hotel", action = "Index" }
);
app.MapControllerRoute(
    name: "hotelDetail",
    pattern: "hotel-detail",
    defaults: new { controller = "Hotel", action = "Detail" }
);

app.MapControllerRoute(
    name: "schemes",
    pattern: "scheme-page",
    defaults: new { controller = "scheme", action = "Index" }
);

app.MapControllerRoute(
    name: "schemeDetail",
    pattern: "scheme-detail",
    defaults: new { controller = "scheme", action = "Detail" }
);

await app.RunAsync();
