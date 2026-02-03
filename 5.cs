using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace UserRegistrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система реєстрації та входу ===");

            Console.Write("Введіть логін: ");
            string username = Console.ReadLine();

            Console.Write("Введіть пароль: ");
            string password = Console.ReadLine();

            // Перевірки (опціонально)
            if (password.Length < 6)
            {
                Console.WriteLine("Пароль повинен містити щонайменше 6 символів.");
                return;
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                Console.WriteLine("Пароль повинен містити хоча б одну цифру.");
                return;
            }

            // Хешування пароля (спрощено, для навчання)
            string hashedPassword = HashPassword(password);

            using (var context = new AppDbContext())
            {
                // Перевірка чи користувач існує
                var existingUser = context.Users.FirstOrDefault(u => u.Username == username);
                if (existingUser != null)
                {
                    Console.WriteLine("Користувач з таким логіном вже існує.");
                    return;
                }

                // Додавання нового користувача
                var user = new User
                {
                    Username = username,
                    Password = hashedPassword,
                    CreatedAt = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();

                Console.WriteLine("Користувача успішно зареєстровано!");
            }
        }

        static string HashPassword(string password)
        {
            // Простий хеш для прикладу (у реальному додатку використовуйте, наприклад, BCrypt або ASP.NET Core Identity)
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
