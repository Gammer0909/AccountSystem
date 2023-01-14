using System.IO;
using System;
using System.Text;

class ConsoleApplication    
{
    bool hasAccount;
    string? accountName;
    int accountPin;
    const string path = "data.dat";
    public ConsoleApplication()
    {
        using (var newStream = File.Open(path, FileMode.Create))
        {
            BinaryReader reader = new BinaryReader(newStream, Encoding.UTF8, false);
            while (reader.PeekChar() != -1)
            {
                accountName = reader.ReadString();
                accountPin = reader.ReadInt32();
   
            }
        }
    }

    public static void Main()
    {
        ConsoleApplication mainInstance = new ConsoleApplication();
        
            if (!File.Exists(path))
            {
                using (var newStream = File.Open(path, FileMode.Create))
                {
                    Console.WriteLine("It seems you do not have an account with us!\nPlease make one.");
                    Console.WriteLine("First, your Account name: ");
                    mainInstance.accountName = Console.ReadLine();
                    Console.WriteLine("Now for your pin [4 or more Numbers]: ");
                    mainInstance.accountPin = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Just a moment while your account information gets saved...");
                    mainInstance.hasAccount = true;
                    var writer = new BinaryWriter(newStream, Encoding.UTF8, false);
                    writer.Write(mainInstance.accountName);
                    writer.Write(mainInstance.accountPin);
                    writer.Write(mainInstance.hasAccount);
                    writer.Close();
                    writer.Dispose();
                    Console.WriteLine("Welcome back!\n\n\nWell, um, that's it. I spent all my time writing the account system. Check back later.\n\n");
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                }

            } else if (File.Exists(path))
            {

                bool trynaHack = SignIn(0);
                if (!trynaHack)
                {
                    Console.WriteLine("Welcome back!\n\n\nWell, um, that's it. I spent all my time writing the account system. Check back later.\n\n");
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                } else if (trynaHack)
                {

                    Console.WriteLine("HEY! You're a hacker! >:I Get out of here.");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();

                }

            }
 
    }

    public static bool SignIn(int guessCount)
    {
        bool areHacker;
        ConsoleApplication methodInstance = new ConsoleApplication();
        Console.WriteLine("Welcome back" + methodInstance.accountName + "!");
        Console.WriteLine("Please enter your PIN to continue.\nPIN:");
        int pinGuess = Convert.ToInt32(Console.ReadLine());
            if (Convert.ToBoolean(pinGuess = methodInstance.accountPin))
            {

                
                areHacker = false;
                return areHacker;

            } else
            {
                guessCount++;
                Console.WriteLine("Please Try again.");
                if (guessCount < 3)
                {

                    SignIn(guessCount);

                } else
                {

                    Console.WriteLine("You have exceeded your maximum guesses! Please try again later.");
                    Console.ReadKey();
                    areHacker = true;
                    return areHacker;

                }
                return false;
            }


        
    }

}