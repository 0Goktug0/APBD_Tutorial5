using Tutorial4.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName); // Use full type names in schema IDs
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

StaticDbContext.Animals = new List<Tutorial4.Animal>();
StaticDbContext.Visits = new List<Tutorial4.Visit>(); 


app.MapGet("/animals", () => StaticDbContext.Animals)
    .WithName("GetAllAnimals");

app.MapGet("/animals/{id}", (int id) => 
{
    var animal = StaticDbContext.Animals.FirstOrDefault(a => a.Id == id);
    if (animal != null)
    {
        return Results.Ok(animal);
    }
    else
    {
        return Results.NotFound();
    }
});


app.MapPost("/animals", (Tutorial4.Animal animal) => {
        StaticDbContext.Animals.Add(animal);
        return Results.Created($"/animals/{animal.Id}", animal);
    })
    .WithName("AddAnimal");

app.MapPut("/animals/{id}", (int id, Animal update) => {
        var animal = StaticDbContext.Animals.FirstOrDefault(a => a.Id == id);
        if (animal is null) return Results.NotFound();
        animal.Name = update.Name;
        animal.Category = update.Category;
        animal.Weight = update.Weight;
        animal.FurColor = update.FurColor;
        return Results.NoContent();
    })
    .WithName("EditAnimal");

app.MapDelete("/animals/{id}", (int id) => {
        var animalIndex = StaticDbContext.Animals.FindIndex(a => a.Id == id);
        if (animalIndex == -1) return Results.NotFound();
        StaticDbContext.Animals.RemoveAt(animalIndex);
        return Results.NoContent();
    })
    .WithName("DeleteAnimal");

// Map endpoints for managing Visits
app.MapGet("/animals/{animalId}/visits", (int animalId) => 
        StaticDbContext.Visits.Where(v => v.AnimalId == animalId))
    .WithName("GetVisitsForAnimal");

app.MapPost("/visits", (Tutorial4.Visit visit) => {
        StaticDbContext.Visits.Add(visit);
        return Results.Created($"/visits/{visit.Id}", visit);
    })
    .WithName("AddVisit");

app.Run();

record Animal(int Id, string Name, string Category, double Weight, string FurColor);
record Visit(int Id, DateTime DateOfVisit, string Description, decimal Price, int AnimalId);