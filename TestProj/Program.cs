using System.IO;
using System;
using System.Text;

public class ConsoleApplication
{
    static bool hasAccount;
    static string? accountName;
    static int accountPin;

    static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString(), "Gammer0909", "data.dat");
    static readonly int guessCount = 3;

    public static void Main()
    {
        Account? accountData = GetAccountData(path);

        if (accountData == null)
        {
            accountData = GetUserAccountInput();
        }
        else
        {
            bool trynaHack = SignIn(accountData);

            if (!trynaHack)
            {
                Console.WriteLine("Welcome back!\n\n\nWell, um, that's it. I spent all my time writing the account system. Check back later.\n\n");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            else if (trynaHack)
            {
                Console.WriteLine("HEY! You're a hacker! >:I Get out of here.");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        Console.WriteLine(accountData);
    }

    private static Account? GetAccountData(string path)
    {
        List<Account> accountList = new List<Account>();

        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);
            using (StringReader reader = new StringReader(fileContents))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    accountName = reader.ReadLine();
                    accountPin = Convert.ToInt32(reader.ReadLine());

                    accountList.Add(new Account { AccountName = accountName, AccountPin = accountPin });
                }
            }

            return accountList.First();
        }

        return null;
    }

    public static bool SignIn(Account account)
    {
        bool areHacker;
        int attempts = 1;

        ConsoleApplication methodInstance = new ConsoleApplication();
        Console.WriteLine("Welcome back" + account.AccountName + "!");
        Console.WriteLine("Please enter your PIN to continue.\nPIN:");

        int pinGuess = Convert.ToInt32(Console.ReadLine());

        if (pinGuess == account.AccountPin)
        {
            areHacker = false;
            return areHacker;
        }

        Console.WriteLine("Please Try again.");

        if (attempts <= guessCount)
        {
            SignIn(account);
        }
        else
        {
            Console.WriteLine("You have exceeded your maximum guesses! Please try again later.");
            Console.ReadKey();

            areHacker = true;
            return areHacker;
        }

        return false;
    }

    private static Account GetUserAccountInput()
    {
        Account account = new Account();

        using (var newStream = File.Open(path, FileMode.Create))
        {
            Console.WriteLine("It seems you do not have an account with us!\nPlease make one.");
            Console.WriteLine("First, your Account name: ");
            account.AccountName = Console.ReadLine();

            Console.WriteLine("Now for your pin [4 or more Numbers]: ");
            account.AccountPin = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Just a moment while your account information gets saved...");
            hasAccount = true;

            using (var writer = new BinaryWriter(newStream, Encoding.UTF8, false))
            {
                writer.Write(account.AccountName);
                writer.Write(account.AccountPin);
                writer.Write(hasAccount);
            }
        }

        return account;
    }
}

public class Account
{
    public string? AccountName { get; set; }

    public int AccountPin { get; set; }
}
