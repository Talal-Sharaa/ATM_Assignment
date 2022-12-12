using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.ComponentModel;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ATM_Assignment
{
    [Serializable]
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
        static List<BankAccount> bankAccounts = new List<BankAccount>();
        public Bank() {}
        public Bank(int bankCapacity) { }
        public void AddNewAccount(BankAccount tmpAccount)
        {
            bankAccounts.Add(tmpAccount);
            if (tmpAccount is null)
            {
                throw new ArgumentNullException(nameof(tmpAccount));
            }
        }
        public int NumberOfCustomers() 
        { 
            return bankAccounts.Count;
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
            for (int i = 0; i < bankAccounts.Count; i++)
            {
                if (bankAccounts[i].CardNumber == cardNumber && bankAccounts[i].PinCode == pinCode)
                {
                    return true;
                }
            }
            return false;

        }
        public int CheckBalance(string cardNumber, string pinCode)
        {

            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException($"'{nameof(cardNumber)}' cannot be null or empty.", nameof(cardNumber));
            }

            if (string.IsNullOrEmpty(pinCode))
            {
                throw new ArgumentException($"'{nameof(pinCode)}' cannot be null or empty.", nameof(pinCode));
            }
            for (int i = 0; i < bankAccounts.Count; i++)
            {
                if (bankAccounts[i].CardNumber == cardNumber && bankAccounts[i].PinCode == pinCode)
                {
                    return bankAccounts[i].AccountBalance;
                }
            }
            return 0;
        }
        public void Withdraw(BankAccount bankAccount, int withdrawAmount)
        {
            if (bankAccount is null)
            {
                throw new ArgumentNullException(nameof(bankAccount));
            }
            bankAccount.AccountBalance = bankAccount.AccountBalance - withdrawAmount;

        }
        public void Deposit(BankAccount bankAccount, int depositamount)
        {
            if (bankAccount is null)
            {
                throw new ArgumentNullException(nameof(bankAccount));
            }
            bankAccount.AccountBalance = bankAccount.AccountBalance + depositamount;
        }
        public void Save(BankAccount bankAccount)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Append, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, bankAccount);
            stream.Close();
        }
        public void Load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            while (stream.Position < stream.Length)
            {
                BankAccount bankAccount = (BankAccount)formatter.Deserialize(stream);
                bankAccounts.Add(bankAccount);
            }
            stream.Close();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank bank1= new Bank();
            bank1.Load();
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
                    string addUser = "y";
                    while (addUser == "y")
                    {

                        Console.WriteLine("Enter the Credit Card Number");
                        string cardNumber = Console.ReadLine();
                        Console.WriteLine("Enter the Pin Code");
                        string pinCode = Console.ReadLine();
                        Console.WriteLine("Enter user Email");
                        string email = Console.ReadLine();
                        Person person = new Person();
                        BankAccount bankAccount = new BankAccount(person, email, cardNumber, pinCode, 0);
                        Bank bank = new Bank(bankCapacity);
                        bank.AddNewAccount(bankAccount);
                        bank.Save(bankAccount);
                        Console.WriteLine("Do you want to add another user?");
                        addUser= Console.ReadLine();
                        if (bank.NumberOfCustomers() >= 10)
                        {
                            Console.WriteLine("Bank already full");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Admin username or password");
                }
            }

            if (select == 2)
            {
                Console.WriteLine("Please Enter Your Card Number");
                string cardNumber = Console.ReadLine();
                Console.WriteLine("Please Enter Your Pin Code");
                string pinCode = Console.ReadLine();
                Bank bank = new Bank();
                if (bank.IsBankUser(cardNumber, pinCode))
                {
                    Console.WriteLine(bank.CheckBalance(cardNumber, pinCode));
                }
                else
                {
                    Console.WriteLine("Wrong Card number or Pin code");
                }
            }
            if (select == 3)
            {
                Console.WriteLine("Please Enter Your Card Number");
                string cardNumber = Console.ReadLine();
                Console.WriteLine("Please Enter Your Pin Code");
                string pinCode = Console.ReadLine();
                Bank bank = new Bank();
                if (bank.IsBankUser(cardNumber, pinCode))
                {
                    Console.WriteLine("Enter Amount of deposit");
                    int deposit = int.Parse(Console.ReadLine()); 
                    List<BankAccount> bankAccounts = new List<BankAccount>();
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                    while (stream.Position < stream.Length)
                    {
                        BankAccount bankAccount = (BankAccount)formatter.Deserialize(stream);
                        bankAccounts.Add(bankAccount);
                    }
                    stream.Close();
                    for(int i = 0; i < bankAccounts.Count; i++)
                    {
                        if (bankAccounts[i].CardNumber== cardNumber && bankAccounts[i].PinCode == pinCode) 
                        {
                            bank.Deposit(bankAccounts[i], deposit);
                        }
                    }
                    for(int i = 0; i < bankAccounts.Count; i++)
                    {
                        IFormatter formatter1 = new BinaryFormatter();
                        Stream stream1 = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream1, bankAccounts[i]);
                    }
                    stream.Close();
                }
                else
                {
                    Console.WriteLine("Wrong Card number or Pin code");
                }
            }
            if (select == 4)
            {
                Console.WriteLine("Please Enter Your Card Number");
                string cardNumber = Console.ReadLine();
                Console.WriteLine("Please Enter Your Pin Code");
                string pinCode = Console.ReadLine();
                Bank bank = new Bank();
                if (bank.IsBankUser(cardNumber, pinCode))
                {
                    Console.WriteLine("Enter Amount of withdrawl");
                    int withdraw = int.Parse(Console.ReadLine());
                    List<BankAccount> bankAccounts = new List<BankAccount>();
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                    while (stream.Position < stream.Length)
                    {
                        BankAccount bankAccount = (BankAccount)formatter.Deserialize(stream);
                        bankAccounts.Add(bankAccount);
                    }
                    stream.Close();
                    for (int i = 0; i < bankAccounts.Count; i++)
                    {
                        if (bankAccounts[i].CardNumber == cardNumber && bankAccounts[i].PinCode == pinCode)
                        {
                            bank.Withdraw(bankAccounts[i], withdraw);
                        }
                    }
                    for (int i = 0; i < bankAccounts.Count; i++)
                    {
                        IFormatter formatter1 = new BinaryFormatter();
                        Stream stream1 = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                        formatter.Serialize(stream1, bankAccounts[i]);
                    }
                    stream.Close();
                }
                else
                {
                    Console.WriteLine("Wrong Card number or Pin code");
                }
            }
        }
    }
}
