using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrdSpel.DAL.Data.SeededData
{
    //skapa standardusers
    public static class SeededUserData
    {
        public static async Task SeedUserAsync(UserManager<IdentityUser> userManager)
        {
            //om det inte finns en user med användarnamn "spelare1", skapa:
            if (await userManager.FindByNameAsync("spelare1") == null)
            {
                var user = new IdentityUser { UserName = "spelare1", EmailConfirmed = true };
                await userManager.CreateAsync(user, "123");
            }

            //om det inte finns en user med användarnamn "spelare2", skapa:
            if (await userManager.FindByNameAsync("spelare2") == null)
            {
                var user = new IdentityUser { UserName = "spelare2", EmailConfirmed = true };
                await userManager.CreateAsync(user, "123");
            }




            // Suggestion Version: A more robust seeding approach with error handling and support for multiple users.
            //var users = new List<(string UserName, string Email)>
            //{
            //    ("spelare1", "spelare1@test.com"),
            //    ("spelare2", "spelare2@test.com")
            //};

            //const string defaultPassword = "Test123!";

            //foreach (var userData in users)
            //{
            //    var existingUser = await userManager.FindByNameAsync(userData.UserName);

            //    if (existingUser != null)
            //    {
            //        continue;
            //    }

            //    var user = new IdentityUser
            //    {
            //        UserName = userData.UserName,
            //        Email = userData.Email,
            //        EmailConfirmed = true
            //    };

            //    var result = await userManager.CreateAsync(user, defaultPassword);

            //    if (!result.Succeeded)
            //    {
            //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            //        throw new InvalidOperationException(
            //            $"Failed to create seeded user '{userData.UserName}': {errors}");
            //    }
            //}
        }
    }
}
