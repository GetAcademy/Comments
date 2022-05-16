using System.Text.Json;
using Comments.Model;

var curDir = AppDomain.CurrentDomain.BaseDirectory + @"\comments";

string GetCommentFilePath(Guid id) => curDir + "\\" + id + ".json";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/comments", async () =>
{
    var comments = new List<Comment>();
    foreach (var filename in Directory.GetFiles(curDir))
    {
        var json = await File.ReadAllTextAsync(filename);
        var comment = JsonSerializer.Deserialize<Comment>(json);
        comments.Add(comment);
    }
    return comments.OrderByDescending(c=>c.Created);
});
app.MapPost("/comments", async (Comment comment) =>
{
    var json = JsonSerializer.Serialize(comment);
    var filePath = GetCommentFilePath(comment.Id);
    await File.WriteAllTextAsync(filePath, json);
});
app.MapPut("/comments", async (Comment comment) =>
{
    // Endre en kommentar
});
app.MapDelete("/comments/{id}", async (Guid id) =>
{
    try
    {
        var filePath = GetCommentFilePath(id);
        File.Delete(filePath);
    }
    catch (Exception e)
    {

    }
});
app.UseStaticFiles();
app.Run();
