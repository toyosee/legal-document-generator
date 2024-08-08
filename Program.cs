using CourtDocumentGenerator.Components;
using CourtDocumentGenerator.Services;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient
// builder.Services.AddHttpClient(); // Add this line to register HttpClient

builder.Services.AddSingleton<DocumentService>();
builder.Services.AddSingleton<GeneratedDocumentService>();
builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<DocumentStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
