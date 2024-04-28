
using InterestsAPI.Data;
using InterestsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InterestsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddAuthorization();

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

            app.UseAuthorization();


            // Return all persons with interests
            app.MapGet("/Persons/Interests", async (ApplicationDbContext context) =>
            {
                var persons = await context.Persons.Include(p => p.Interests) 
                    .ThenInclude(i => i.Links) 
                    .ToListAsync();

                if (persons == null || !persons.Any())
                {
                    return Results.NotFound("Hittade inga personer med intressen");
                }

                return Results.Ok(persons);
            });

            // create Person with interests
            app.MapPost("/persons", async (Person person, ApplicationDbContext context) =>
            {
                foreach (var interest in person.Interests)
                {
                    foreach (var link in interest.Links)
                    {
                        context.Links.Add(link);
                    }
                    context.Interests.Add(interest);
                }
                context.Persons.Add(person);

                await context.SaveChangesAsync();

                return Results.Created($"/persons/{person.PersonId}", person);
            });

            // Get interests and links for a specific person
            app.MapGet("/Interests", async (ApplicationDbContext context) =>
            {
                var interests = await context.Interests.Include(i => i.Links).ToListAsync();

                if (interests == null)
                {
                    return Results.NotFound("Person not found");
                }

                return Results.Ok(interests);
            });

            // add more interests to a person
            app.MapPatch("/Persons/{personId}", async (int personId, Person updatedPerson, ApplicationDbContext context) =>
            {
                var existingPerson = await context.Persons.Include(p => p.Interests)
                    .ThenInclude(i => i.Links)
                    .FirstOrDefaultAsync(p => p.PersonId == personId);

                if (existingPerson == null)
                {
                    return Results.NotFound("Person not found");
                }
                if (updatedPerson.Interests != null)
                {
                    foreach (var updatedInterest in updatedPerson.Interests)
                    {
                        existingPerson.Interests.Add(updatedInterest);
                    }
                }

                await context.SaveChangesAsync();

                return Results.Ok(existingPerson);
            });

            // update a interest
            app.MapPut("/Interests/{id:int}", async (int id, Interest updatedInterest, ApplicationDbContext context) =>
            {
                var existingInterest = await context.Interests.Include(i => i.Links).FirstOrDefaultAsync(i => i.InterestId == id);

                if (existingInterest == null)
                {
                    return Results.NotFound("Interest not found");
                }
                existingInterest.InterestName = updatedInterest.InterestName;
                existingInterest.InterestDescription = updatedInterest.InterestDescription;

                foreach (var link in updatedInterest.Links)
                {
                    existingInterest.Links.Add(link);
                }

                await context.SaveChangesAsync();

                return Results.Ok(existingInterest);
            });

            app.Run();
        }
    }
}
