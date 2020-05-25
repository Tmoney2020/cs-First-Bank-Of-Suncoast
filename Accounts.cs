namespace cs_First_Bank_Of_Suncoast
{
    class Transactions
    {
        public int Deposit
        {
            get; set;
        }

        public int Withdraw
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string TransactionType
        {
            get; set;
        }

        public int Balance()
        {
            var balance = Deposit - Withdraw;

            return balance;
        }
    }
}
