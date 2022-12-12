using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace ATM_Assignment
{
    public class Person
    {
    }
    [Serializable]
    public class BankAccount
    {
        public BankAccount(Person person, string email, string cardNumber, string pinCode, int accountBalance)
        {
            Person = person;
            Email = email;
            CardNumber = cardNumber;
            PinCode = pinCode;
            AccountBalance = accountBalance;
        }

        public Person Person { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string PinCode { get; set; }
        public int AccountBalance { get; set; }
    }
    public class Bank 
    {
        static int count;
        public Bank() {}
        public Bank(int bankCapacity) { }
        public void AddNewAccount(BankAccount tmpAccount)
        {
            count++;
            if (tmpAccount is null)
            {
                throw new ArgumentNullException(nameof(tmpAccount));
            }
        }
        public int NumberOfCustomers() 
        { 
            return count;
        }

        public bool IsBankUser(string cardNumber, string pinCode)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException($"'{nameof(cardNumber)}' cannot be null or empty.", nameof(cardNumber));
            }

            if (string.IsNullOrEmpty(pinCode))
            {
                throw new ArgumentException($"'{nameof(pinCode)}' cannot be null or empty.", nameof(pinCode));
            }

            return false;
        }
        public int CheckBalance(string CardNumber, string pinCode)
        {
            if (string.IsNullOrEmpty(CardNumber))
            {
                throw new ArgumentException($"'{nameof(CardNumber)}' cannot be null or empty.", nameof(CardNumber));
            }

            if (string.IsNullOrEmpty(pinCode))
            {
                throw new ArgumentException($"'{nameof(pinCode)}' cannot be null or empty.", nameof(pinCode));
            }

            return 100;
        }
        public void Withdraw(BankAccount bankAccount, int withdrawAmount)
        {
            if (bankAccount is null)
            {
                throw new ArgumentNullException(nameof(bankAccount));
            }
        }
        public void Deposit(BankAccount bankAccount, int depositamount)
        {
            if (bankAccount is null)
            {
                throw new ArgumentNullException(nameof(bankAccount));
            }
        }
        public void Save()
        {
        }
        public void Load()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please select an option");
            Console.WriteLine("1- Add A new Account");
            Console.WriteLine("2- Check Account Balance");
            Console.WriteLine("3- Withdraw Money");
            Console.WriteLine("4- Deposit Money");
            int select = int.Parse(Console.ReadLine());
            if (select == 1) 
            {
                Console.WriteLine("Enter Admin username");
                string userName = Console.ReadLine();
                Console.WriteLine("Enter Admin Password");
                string password = Console.ReadLine();
                if(userName == "Admin" && password == "1234")
                {
                    int bankCapacity = 10;
                    Console.WriteLine("Enter the Credit Card Number");
                    string cardNumber = Console.ReadLine();
                    Console.WriteLine("Enter the Pin Code");
                    string pinCode = Console.ReadLine();
                    Console.WriteLine("Enter user Email");
                    string email = Console.ReadLine();
                    Person person= new Person();
                    BankAccount bankAccount = new BankAccount(person, email, cardNumber, pinCode, 0);
                    Bank bank = new Bank(bankCapacity);
                    bank.AddNewAccount(bankAccount);
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, bankAccount);
                    stream.Close();
                }
                else
                {
                    Console.WriteLine("Wrong username or password");
                }
            }
        }
    }
}
