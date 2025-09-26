using KauppaAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var Kauppakanta = new Kauppakanta();

app.MapGet("/", () => "Hello World!");
app.MapGet("/asiakkaat", () => Kauppakanta.HaeAsiakkaat());

app.MapPost("/asiakkaat", (Asiakas asiakas) =>
{
    Kauppakanta.LisaaAsiakas(asiakas.Nimi);
    return Kauppakanta.HaeAsiakkaat();
});

app.Run();
