using System.Text.Json;
using Comments.Model;

// Pause til 10:38

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/comments", () =>
{
    var curDir = AppDomain.CurrentDomain.BaseDirectory + @"\comments";
    return Directory.GetFiles(curDir)
        .Select(filename=>
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<Comment>(json);
        });
});
app.Run();
