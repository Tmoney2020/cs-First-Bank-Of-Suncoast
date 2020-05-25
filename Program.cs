using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace cs_First_Bank_Of_Suncoast
{
    class Program
    {
        static void Main(string[] args)
        {


            static string PromptForString(string prompt)
            {
                Console.Write(prompt);
                var userInput = Console.ReadLine();

                return userInput;
            }

            static int PromptForInteger(string prompt)
            {
                Console.Write(prompt);
                int userInput;
                var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

                if (isThisGoodInput)
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                    return 0;
                }
            }


            var listOfTransactions = new List<Transactions>();

            if (File.Exists("logOfTransactions.csv"))
            {
                var reader = new StreamReader("logOfTransactions.csv");

                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

                listOfTransactions = csvReader.GetRecords<Transactions>().ToList();

            }
            // savings 
            var whatSavingsAccount = listOfTransactions.Where(Transactions => Transactions.TransactionType == "deposit" && Transactions.Type == "savings");
            var whatSavingsAccountTwo = listOfTransactions.Where(Transactions => Transactions.TransactionType == "withdraw" && Transactions.Type == "savings");

            var savingsAccountDeposit = whatSavingsAccount.Sum(Transactions => Transactions.Deposit);
            var savingsAccountWithdraw = whatSavingsAccountTwo.Sum(Transactions => Transactions.Withdraw);

            var beginningBalanceSavings = savingsAccountDeposit - savingsAccountWithdraw;


            //checking 
            var whatCheckingAccount = listOfTransactions.Where(Transactions => Transactions.TransactionType == "deposit" && Transactions.Type == "checking");
            var whatCheckingAccountTwo = listOfTransactions.Where(Transactions => Transactions.TransactionType == "withdraw" && Transactions.Type == "checking");

            var checkingAccountDeposit = whatCheckingAccount.Sum(Transactions => Transactions.Deposit);
            var checkingAccountWithdraw = whatCheckingAccountTwo.Sum(Transactions => Transactions.Withdraw);

            var beginningBalanceChecking = checkingAccountDeposit - checkingAccountWithdraw;

            Console.WriteLine();
            Console.WriteLine($"The current balance of your checking account is {beginningBalanceChecking}");
            Console.WriteLine($"The current balance of your savings account is {beginningBalanceSavings}");

            var youChooseToQuit = false;

            while (youChooseToQuit == false)
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"Welcome to SunCoast Bank!");
                Console.WriteLine("---------------------------------");

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Hello Trent");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine($"Please choose an option below by selecting the letter of that option in the ()");
                Console.WriteLine();

                Console.WriteLine($"(D)eposit money into your account");
                Console.WriteLine();

                Console.WriteLine($"(W)ithdraw money from your account");
                Console.WriteLine();

                // Console.WriteLine($"(T)ransfer money from one account to another");
                // Console.WriteLine();

                Console.WriteLine($"(B)alance of your accounts");
                Console.WriteLine();

                Console.WriteLine($"(E)xit your bank account");
                Console.WriteLine();

                var toChoose = PromptForString($"Choose: ");

                //Exiting the application
                if (toChoose == "E")
                {
                    youChooseToQuit = true;
                    Console.WriteLine($"Thank you for banking at SunCoast Bank! Keep the change you filthy animal!");
                    Console.WriteLine();
                }
                // transfer money from one account to another (in progress)
                // if (toChoose == "T")
                // {
                //     var checkingOrSavings = PromptForString("Which account would you like to transfer money from (checking/savings): ");
                //     if (checkingOrSavings == "savings")
                //     {
                //         var amountToTransfer = PromptForInteger($"Amount to transfer: ");
                //         var absoluteAmountTotransfer = Math.Abs(amountToTransfer);

                //         var newTransaction = new Transactions
                //         {
                //             Deposit = 0,
                //             Withdraw = absoluteAmountTotransfer,
                //             Type = "savings"
                //         };

                //         var newTransactionTwo = new Transactions
                //         {
                //             Deposit = absoluteAmountTotransfer,
                //             Withdraw = 0,
                //             Type = "checking"
                //         };

                //         listOfSavingsAccount.Add(newTransaction);
                //         listOfCheckingAccount.Add(newTransactionTwo);
                //     }
                //     if (checkingOrSavings == "checking")
                //     {
                //         var amountToTransfer = PromptForInteger($"Amount to transfer: ");
                //         var absoluteAmountTotransfer = Math.Abs(amountToTransfer);

                //         var newTransaction = new Transactions
                //         {
                //             Deposit = 0,
                //             Withdraw = absoluteAmountTotransfer,
                //             Type = "checking"
                //         };

                //         var newTransactionTwo = new Transactions
                //         {
                //             Deposit = absoluteAmountTotransfer,
                //             Withdraw = 0,
                //             Type = "saving"
                //         };

                //         listOfSavingsAccount.Add(newTransactionTwo);
                //         listOfCheckingAccount.Add(newTransaction);
                //     }


                // }

                //to show your balance 
                if (toChoose == "B")
                {

                    var mySavingsAccount = listOfTransactions.Where(Transactions => Transactions.Type == "savings");
                    var myCheckingAccount = listOfTransactions.Where(Transactions => Transactions.Type == "checking");

                    var newSavingsAccountBalance = mySavingsAccount.Sum(Transactions => Transactions.Deposit - Transactions.Withdraw);
                    Console.WriteLine($"The balance of your savings account is {newSavingsAccountBalance}");

                    var newCheckingAccountBalance = myCheckingAccount.Sum(Transactions => Transactions.Deposit - Transactions.Withdraw);
                    Console.WriteLine($"The balance of your checking account is {newCheckingAccountBalance}");

                }

                //withdraw money
                if (toChoose == "W")
                {
                    var checkingOrSavings = PromptForString("Which account would you like to withdraw money from (checking/savings): ");
                    {
                        var mySavingsAccount = listOfTransactions.Where(Transactions => Transactions.Type == "savings");
                        var myCheckingAccount = listOfTransactions.Where(Transactions => Transactions.Type == "checking");

                        var newSavingsAccountBalance = mySavingsAccount.Sum(Transactions => Transactions.Deposit - Transactions.Withdraw);
                        var newCheckingAccountBalance = myCheckingAccount.Sum(Transactions => Transactions.Deposit - Transactions.Withdraw);
                        if (checkingOrSavings == "savings")
                        {
                            var amountToWithdraw = PromptForInteger($"Amount to withdraw: ");
                            var absoluteAmountToWithdraw = Math.Abs(amountToWithdraw);

                            if (absoluteAmountToWithdraw > newSavingsAccountBalance)
                            {
                                Console.WriteLine($"I'm sorry this would overdraft your account. Please choose again.");
                            }
                            else
                            {

                                var newTransaction = new Transactions
                                {
                                    Deposit = 0,
                                    Withdraw = absoluteAmountToWithdraw,
                                    Type = "savings",
                                    TransactionType = "withdraw",
                                };

                                listOfTransactions.Add(newTransaction);
                            }
                        }
                        if (checkingOrSavings == "checking")
                        {
                            var amountToWithdraw = PromptForInteger($"Amount to withdraw: ");
                            var absoluteAmountToWithdraw = Math.Abs(amountToWithdraw);

                            if (absoluteAmountToWithdraw > newCheckingAccountBalance)
                            {
                                Console.WriteLine($"I'm sorry this would overdraft your account. Please choose again.");

                            }
                            else
                            {

                                var newTransaction = new Transactions
                                {
                                    Deposit = 0,
                                    Withdraw = absoluteAmountToWithdraw,
                                    Type = "checking",
                                    TransactionType = "withdraw",
                                };

                                listOfTransactions.Add(newTransaction);
                            }
                        }
                    }
                }

                //Deposit your money
                if (toChoose == "D")
                {
                    var checkingOrSavings = PromptForString("Which account would you like to desposit in (checking/savings): ");
                    {
                        if (checkingOrSavings == "savings")
                        {
                            var amountToDeposit = PromptForInteger($"Amount to deposit: ");
                            var absoluteAmountToDeposit = Math.Abs(amountToDeposit);

                            var newTransaction = new Transactions
                            {
                                Deposit = absoluteAmountToDeposit,
                                Withdraw = 0,
                                Type = "savings",
                                TransactionType = "deposit",
                            };

                            listOfTransactions.Add(newTransaction);
                        }

                        if (checkingOrSavings == "checking")
                        {
                            var amountToDeposit = PromptForInteger($"Amount to deposit: ");
                            var absoluteAmountToDeposit = Math.Abs(amountToDeposit);

                            var newTransaction = new Transactions
                            {
                                Deposit = absoluteAmountToDeposit,
                                Withdraw = 0,
                                Type = "checking",
                                TransactionType = "deposit",

                            };

                            listOfTransactions.Add(newTransaction);

                        }

                    }
                }
            }
            var fileWriter = new StreamWriter("logOfTransactions.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(listOfTransactions);

            fileWriter.Close();

        }
    }
}
