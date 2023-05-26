// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using DAL;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder();
options.UseNpgsql("Server=localhost;Port=5444;Database=QuizDb;Uid=Admin;pwd=mysecretpassword");

await using var dbContext = new QuizDb(options.Options);

await new Seeder().SeedDataBase(dbContext);