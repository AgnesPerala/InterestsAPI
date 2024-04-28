
using InterestsAPI.Data;
using InterestsAPI.Models;
using InterestsAPI.Models.DTO;
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


            // Return all persons 
            app.MapGet("/GetAllPersons", async (ApplicationDbContext context) =>
            {
                List<Person> persons = await context.Persons.Include(p => p.Interests)
                    .ThenInclude(i => i.Links) 
                    .ToListAsync();

                if (persons == null || persons.Count == 0)
                {
                    return Results.NotFound("Hittade inga personer med intressen");
                }

                return Results.Ok(persons);
            });

            // create Person 
            app.MapPost("/CreatePerson", async (CreatePerson createPerson, ApplicationDbContext context) =>
            {
                Person person = new();
                person.Age = createPerson.Age;
                person.PersonName = createPerson.PersonName;
                person.PhoneNumber = createPerson.PhoneNumber;

                context.Persons.Add(person);
                await context.SaveChangesAsync();
                return Results.Created($"/persons/{person.PersonId}", person);
            });
            
            // create interest 
            app.MapPost("/CreateInterest", async (CreateInterest createInterest, ApplicationDbContext context) =>
            {
                Interest interest = new();
                interest.InterestName = createInterest.InterestName;
                interest.InterestDescription = createInterest.InterestDescription;
                var person = await context.Persons.FindAsync(createInterest.PersonId);
                if (person == null)
                {
                    return Results.NotFound("Person not found");
                }

                // Koppla det nya intresset till personen
                person.Interests.Add(interest);

                await context.SaveChangesAsync();

                return Results.Created($"/Interest/{interest.InterestId}", interest);
            }); 
            
            // create interest 
            app.MapPost("/CreateLink", async (CreateLink createLink, ApplicationDbContext context) =>
            {
                Link link = new();
                link.Webbsite = createLink.Webbsite;
                Interest interest = await context.Interests.FindAsync(createLink.InterestId);
                if (interest == null)
                {
                    return Results.NotFound("Person not found");
                }

                // Koppla det nya intresset till personen
                interest.Links.Add(link);

                await context.SaveChangesAsync();

                return Results.Created($"/Interest/{link.LinkId}", link);
            });


            // Get interests and links for a specific person
            app.MapGet("/GetInterests/{personId}", async (int personId, ApplicationDbContext context) =>
            {
                Person? person = await context.Persons.Include(p => p.Interests).ThenInclude(i => i.Links).Where(p => p.PersonId == personId).FirstOrDefaultAsync();
                if (person == null)
                {
                    return Results.NotFound("Person not found");
                }

                List<Interest>? interests = person.Interests;
                if (interests == null)
                {
                    return Results.NotFound("interest not found");
                }
                if (interests.Count == 0)
                {

                    return Results.NotFound("Person dont have any interests");
                }

                return Results.Ok(interests);
            }); 
            
            // Get links for a specific person
            app.MapGet("/GetLinks/{personId}", async (int personId, ApplicationDbContext context) =>
            {
                Person? person = await context.Persons.Include(p => p.Interests).ThenInclude(i => i.Links).Where(p => p.PersonId == personId).FirstOrDefaultAsync();
                if (person == null)
                {
                    return Results.NotFound("Person not found");
                }

                List<Interest>? interests = person.Interests;
                if (interests == null)
                {
                    return Results.NotFound("interest not found");
                }
                List<Link> links = new();
                foreach(Interest interest in interests)
                {
                    if(interest.Links is not null)
                    {
                        links.AddRange(interest.Links);
                    }
                }

                return Results.Ok(links);
            });

            app.Run();
        }
    }
}
