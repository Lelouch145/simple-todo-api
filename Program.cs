using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args); // Skapar en builder som används för att konfigurera webbservern

// Registrerar services som applikationen ska använda
builder.Services.AddEndpointsApiExplorer(); // Låter ASP.NET hitta alla endpoints (GET, POST osv.) så Swagger kan dokumentera dem
builder.Services.AddSwaggerGen(); // Skapar Swagger-dokumentationen (OpenAPI JSON)

var app = builder.Build(); // Bygger den färdiga webbapplikationen

// Konfigurerar HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Genererar Swagger-dokumentationen (JSON som beskriver API:t)
    app.UseSwaggerUI(); // Visar ett grafiskt gränssnitt där man kan testa API-endpoints
}

app.UseHttpsRedirection(); // Tvingar API:t att använda HTTPS

// Skapar en lista i minnet som lagrar alla tasks
List<TaskItem> tasks = new List<TaskItem>
{
    new TaskItem { Id = 1, Title = "Learn c#" },       // Första tasken
    new TaskItem { Id = 2, Title = "Build API" },      // Andra tasken
    new TaskItem { Id = 3, Title = "Practice Backend"} // Tredje tasken
};

app.MapGet("/tasks", () => tasks); // GET endpoint som returnerar alla tasks i listan

app.MapPost("/tasks", (TaskItem task) => // POST endpoint som tar emot en task från request body (JSON)
{
    tasks.Add(task); // Lägger till tasken i listan
    return task;     // Returnerar objektet som lades till
});

app.MapGet("/", () => // Root endpoint för att snabbt testa att API:t fungerar
{
    return "My first API is working!";
});

app.MapGet("/tasks/{id}", (int id) => // GET endpoint för att hämta en specifik task via id (t.ex. /tasks/2)
{
    TaskItem? foundTask = null; // Variabel för att lagra tasken vi letar efter

    foreach (TaskItem task in tasks) // Loopar igenom alla tasks i listan
    {
        if (task.Id == id) // Om taskens Id matchar id från URL:en
        {
            foundTask = task; // Sparar tasken som hittades
            break;            // Stoppar loopen när rätt task hittats
        }
    }

    if (foundTask == null) // Om ingen task hittades
    {
        return Results.NotFound("Task not found"); // Returnerar HTTP 404
    }

    return Results.Ok(foundTask); // Returnerar tasken som hittades
});

app.MapDelete("/tasks/{id}", (int id) => // DELETE endpoint för att ta bort en task via id
{
    TaskItem? taskDelete = null; // Variabel för tasken som ska tas bort

    foreach (TaskItem task in tasks) // Loopar igenom listan för att hitta rätt task
    {
        if (task.Id == id)
        {
            taskDelete = task; // Sparar tasken som ska tas bort
            break;             // Stoppar loopen när den hittats
        }
    }

    if (taskDelete == null) // Om tasken inte finns
    {
        return Results.NotFound("Task not found"); // Returnerar HTTP 404
    }
    else
    {
        tasks.Remove(taskDelete); // Tar bort tasken från listan
        return Results.Ok("Task deleted"); // Returnerar ett OK-svar
    }
});

app.Run(); // Startar webbservern

class TaskItem // Modellklass som representerar en task
{
    public int Id { get; set; }      // Unikt id för tasken
    public string Title { get; set; } // Namnet eller beskrivningen av tasken
}