var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Pizza> pizzas = new()
{
    new Pizza(1, "Cheese", false),
    new Pizza(2, "Pepperoni", false),
    new Pizza(3, "Veggie", true),

    // Added extra record for assignment
    new Pizza(4, "Hawaiian", false)
};

app.MapGet("/", () => "Pizza API Running!");

app.MapGet("/pizzas", () => pizzas);

app.MapGet("/pizzas/{id}", (int id) =>
{
    var pizza = pizzas.FirstOrDefault(p => p.Id == id);
    return pizza is not null ? Results.Ok(pizza) : Results.NotFound();
});

app.MapPost("/pizzas", (Pizza pizza) =>
{
    pizzas.Add(pizza);
    return Results.Created($"/pizzas/{pizza.Id}", pizza);
});

app.MapDelete("/pizzas/{id}", (int id) =>
{
    var pizza = pizzas.FirstOrDefault(p => p.Id == id);
    if (pizza is null) return Results.NotFound();

    pizzas.Remove(pizza);
    return Results.NoContent();
});

app.Run();

record Pizza(int Id, string Name, bool IsGlutenFree);