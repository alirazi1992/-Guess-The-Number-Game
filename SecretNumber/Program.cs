using System;

class Program
{
    static void Main()
    {
        Console.Title = "Guess The Number - Day 2";
        do
        {
            PlayOneGame();
        } while (AskYesNo("\nPlay again? (y/n): "));

        Console.WriteLine("Thanks for playing! 👋");
    }

    static void PlayOneGame()
    {
        Console.Clear();
        Console.WriteLine("=== Guess The Number ===");

        // Choose difficulty
        Console.WriteLine("\nChoose difficulty:");
        Console.WriteLine("1) Easy   (1–20,   8 attempts)");
        Console.WriteLine("2) Normal (1–50,   7 attempts)");
        Console.WriteLine("3) Hard   (1–100,  6 attempts)");
        int choice = ReadInt("Enter 1, 2, or 3: ", min: 1, max: 3);

        (int min, int max, int tries) settings = choice switch
        {
            1 => (1, 20, 8),
            2 => (1, 50, 7),
            3 => (1, 100, 6),
            _ => (1, 50, 7)
        };

        var rnd = new Random();
        int secret = rnd.Next(settings.min, settings.max + 1);
        int attemptsLeft = settings.tries;

        Console.WriteLine($"\nI picked a number between {settings.min} and {settings.max}.");
        Console.WriteLine($"You have {attemptsLeft} attempts. Good luck!\n");

        while (attemptsLeft > 0)
        {
            int guess = ReadInt($"Guess ({attemptsLeft} left): ", settings.min, settings.max);

            if (guess == secret)
            {
                Console.WriteLine("🎉 Correct! You guessed the number!");
                return;
            }

            attemptsLeft--;
            if (guess < secret)
                Console.WriteLine("⬆️  Higher!");
            else
                Console.WriteLine("⬇️  Lower!");

            // Small hint zone (optional)
            int diff = Math.Abs(secret - guess);
            if (diff <= (settings.max - settings.min) / 20) // close if within ~5% of range
                Console.WriteLine("🔥 You're very close!");
        }

        Console.WriteLine($"\n❌ Out of attempts. The number was: {secret}");
    }

    // Read an integer safely with optional range validation
    static int ReadInt(string prompt, int? min = null, int? max = null)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                if (min.HasValue && value < min.Value)
                {
                    Console.WriteLine($"Please enter a number ≥ {min.Value}.");
                    continue;
                }
                if (max.HasValue && value > max.Value)
                {
                    Console.WriteLine($"Please enter a number ≤ {max.Value}.");
                    continue;
                }
                return value;
            }
            Console.WriteLine("Invalid number. Try again.");
        }
    }

    // Simple yes/no prompt
    static bool AskYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (input == "y" || input == "yes") return true;
            if (input == "n" || input == "no") return false;
            Console.WriteLine("Please enter 'y' or 'n'.");
        }
    }
}
