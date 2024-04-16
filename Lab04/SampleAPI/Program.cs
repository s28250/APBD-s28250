using SampleAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var _students = new List<Student>
{
    new Student { IdStudent = 1, FirstName = "John", LastName = "Doe", Address = "Warsaw, Zlota 12", Email = "doe@gmail.com"},
    new Student { IdStudent = 2, FirstName = "Jane", LastName = "Smith", Address = "Warsaw, Koszykowa 86", Email = "smith@gmail.com"},
    new Student { IdStudent = 3, FirstName = "Sam", LastName = "Adams", Address = "Warszawa, Nowy Swiat 67", Email = "adams@gmail.com"},
};

app.MapGet("/api/students", () => Results.Ok(_students))
    .WithName("GetStudents")
    .WithOpenApi();

app.MapGet("/api/students/{id:int}", (int id) =>
    {
        var student = _students.FirstOrDefault(s => s.IdStudent == id);
        return student == null ? Results.NotFound($"Student with id {id} was not found") : Results.Ok(student);
    })
    .WithName("GetStudent")
    .WithOpenApi();

app.MapPost("/api/students", (Student student) =>
    {
        _students.Add(student);
        return Results.StatusCode(StatusCodes.Status201Created);
    })
    .WithName("CreateStudent")
    .WithOpenApi();

app.MapPut("/api/students/{id:int}", (int id, Student student) =>
    {
        var studentToEdit = _students.FirstOrDefault(s => s.IdStudent == id);
        if (studentToEdit == null)
        {
            return Results.NotFound($"Student with id {id} was not found");
        }
        _students.Remove(studentToEdit);
        _students.Add(student);
        return Results.NoContent();
    })
    .WithName("UpdateStudent")
    .WithOpenApi();

app.MapDelete("/api/students/{id:int}", (int id) =>
    {
        var studentToDelete = _students.FirstOrDefault(s => s.IdStudent == id);
        if (studentToDelete == null)
        {
            return Results.NoContent();
        }
        _students.Remove(studentToDelete);
        return Results.NoContent();
    })
    .WithName("DeleteStudent")
    .WithOpenApi();

app.Run();
