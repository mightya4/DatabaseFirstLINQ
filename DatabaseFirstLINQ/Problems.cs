using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
                    //ProblemOne();
                    //ProblemTwo();
                    //ProblemThree();
                    //ProblemFour();
                    //ProblemFive();
                    //ProblemSix();
                    //ProblemSeven();
                    //ProblemEight();
                    //ProblemNine();
                    //ProblemTen();
                    //ProblemEleven();
                    //ProblemTwelve();
                    //ProblemThirteen();
                    //ProblemFourteen();
                    //ProblemFifteen();
                    //ProblemSixteen();
                    //ProblemSeventeen();
                    //ProblemEighteen();
                    //ProblemNineteen();
                    //ProblemTwenty();
                    //BonusOne();
                    BonusTwo();
        }

        //        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var numOfUsers = _context.Users.ToList().Count;
            Console.WriteLine($"There are {numOfUsers} users in the Users table");

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.

            var products = _context.Products;
            foreach (Product product in products) {
                if (product.Price > 150)
                    Console.WriteLine($"Product: {product.Name} costs {product.Price}");
                    }

        }

        private void ProblemFour()
        {
    
           
                // Write a LINQ query that gets each product that contains an "s" in the products name.
                // Then print the name of each product from the above query to the console.
                var productNamesStartWithS = _context.Products.Select(p => p.Name).ToList();
                Console.WriteLine("-------------------------------------------\n");
                foreach (var product in productNamesStartWithS)
                {
                    if (product.ToLower().Contains("s"))
                    {
                        Console.WriteLine($"{product}");
                    };
                    ;
                }
                Console.WriteLine("-------------------------------------------\n");
            }

        

        private void ProblemFive()
        {
                // write a linq query that gets all of the users who registered before 2016
                // then print each user's email and registration date to the console.
                var dateString = DateTime.Parse("01 Jan 2016");
                var UsersBefore2016 = _context.Users.ToList().FindAll(m => m.RegistrationDate < dateString);
                Console.WriteLine("-------------------------------------------\n");
                Console.WriteLine("Users who registered before 2016");
                foreach (var user in UsersBefore2016)
                {
                    Console.WriteLine($" Email: {user.Email} Registration Date: {user.RegistrationDate}");
                }
                Console.WriteLine("\n-------------------------------------------");
        }
        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var dateStringAfter2016 = DateTime.Parse("31 Dec 2016");
            var dateStringBefore2018 = DateTime.Parse("01 Jan 2018");
            var UsersBefore2018After2016 = _context.Users.ToList().FindAll(m => m.RegistrationDate < dateStringBefore2018).FindAll(m => m.RegistrationDate > dateStringAfter2016);
             Console.WriteLine("-------------------------------------------\n");
            Console.WriteLine("Users who registered after 2016 and before 2018 ");
            foreach (var user in UsersBefore2018After2016)
            {
                Console.WriteLine($"Email: {user.Email} Registration Date: {user.RegistrationDate}");
            }


        }

        //        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var customerUsers = _context.ShoppingCarts;
                    var products = customerUsers.Include(u => u.User).Include(p => p.Product).Where(u => u.User.Email == "afton@gmail.com");
                    Console.WriteLine("-------------------------------------------\n");
                    foreach (var product in products)
            {
                    Console.WriteLine($"Product: {product.Product.Name} Price: {product.Product.Price} Quantity: {product.Quantity}");
            }
                    Console.WriteLine("-------------------------------------------\n");

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var customerUsers = _context.ShoppingCarts;
            var sumOfProductsPrices = customerUsers.Include(u => u.User).Include(p => p.Product).Where(u => u.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine("-------------------------------------------\n");
            Console.WriteLine($"The sum of the cart is ${sumOfProductsPrices}.");
            Console.WriteLine("-------------------------------------------\n");
        }
        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            var employees = _context.UserRoles.Include(u => u.User).Include(r => r.Role).Where(e => e.RoleId == 2).Select(u => u.UserId);
            var products = _context.ShoppingCarts.Include(u => u.User).Include(p => p.Product).Where(u => employees.Contains(u.UserId));
            Console.WriteLine("-------------------------------------------\n");
            foreach (var item in products)
            {
                Console.WriteLine($"{item.User.Email} {item.Product.Name} {item.Product.Price} {item.Quantity}");
            }
            Console.WriteLine("-------------------------------------------\n");
        }

    //        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

    //        // <><> C Actions (Create) <><>

    private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Rice Krispies",
                Description = "Cereal Marshmallow Squares",
                Price = 50
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Rice Krispies").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCartItem = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId
            };
            _context.ShoppingCarts.Add(newShoppingCartItem);
            _context.SaveChanges();


        }

        //        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(u => u.Price == 50).SingleOrDefault();
            product.Price = 100;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        //        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }
        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var deleteUser = _context.Users.Where(sc => sc.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(deleteUser);
            _context.SaveChanges();
        }

        //        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.Write("Enter your email: ");
            var getUserEmail = Console.ReadLine().ToLower();
            Console.Write("Enter your password: ");
            var getUserPass = Console.ReadLine();
            var validateUserEmail = _context.Users.Where(sc => sc.Email == getUserEmail).Where(sc => sc.Password == getUserPass);
            if (validateUserEmail.Any())
            {
                Console.WriteLine("Signed In!");
            }
            else
            {
                Console.WriteLine("Invalid Email or Password.");
            }
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var customerUsers = _context.ShoppingCarts;
            var listOfUserByEmail = _context.Users.ToList();
            var totalOfEachUserShoppingCart = customerUsers.Include(u => u.User).Include(p => p.Product).Select(sc => sc.Product.Price);
            foreach (var total in totalOfEachUserShoppingCart)
            {
                
                Console.WriteLine($"The sum of the total every user cart is ${totalOfEachUserShoppingCart}.");
            }
            
            Console.WriteLine("-------------------------------------------\n");
            var totalProductsPricesList = customerUsers.Include(u => u.User).Include(p => p.Product).Select(sc => sc.Product.Price).Sum();
            Console.WriteLine($"The sum of the total every user cart is ${totalProductsPricesList}.");
            Console.WriteLine("-------------------------------------------\n");


            foreach(var userEmail in listOfUserByEmail)
            {
                var priceOfCurrentUserShoppingCart = customerUsers.Include(u => u.User).Include(p => p.Product).Where(u => u.User.Email == userEmail.Email).Select(sc => sc.Product.Price).Sum();
                Console.WriteLine($"User email: {userEmail.Email} | Total of user shopping cart is  ${priceOfCurrentUserShoppingCart}");
            }
            
            

        }

        //        // BIG ONE
        //        private void BonusThree()
        //        {
        //            // 1. Create functionality for a user to sign in via the console
        //            // 2. If the user succesfully signs in
        //                // a. Give them a menu where they perform the following actions within the console
        //                    // View the products in their shopping cart
        //                    // View all products in the Products table
        //                    // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
        //                    // Remove a product from their shopping cart
        //            // 3. If the user does not succesfully sing in
        //                // a. Display "Invalid Email or Password"
        //                // b. Re-prompt the user for credentials

        //        }

    }
}
