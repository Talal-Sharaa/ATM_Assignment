using System;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddBankUser_AddNewUser_OneBankAccountCreated()
        {
            int bankCapacity = 10;
            Person p1 = new Person();

            string pinCode = "1234";
            string cardNumber = "123456789";
            int accountBalance = 100;
            BankAccount tmpAccount = new BankAccount(p1, "Ahmad@test.com", cardNumber, pinCode, accountBalance);


            Bank testBank = new Bank(bankCapacity);

            testBank.AddNewAccount(tmpAccount);

            Assert.AreEqual(testBank.NumberOfCustomers, 1);


        }
    }