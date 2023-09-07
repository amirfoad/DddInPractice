using System;
namespace DddInPractice.Logic
{
    public sealed class SnackMachine
    {
        /// <summary>
        /// The amount of money machine has
        /// </summary>
        public Money MoneyInside { get; private set; }


        /// <summary>
        /// The amount of money user inserted
        /// </summary>
        public Money MoneyInTransaction { get; private set; }

        public void InsertMoney(Money money)
        {
            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            //MoneyInTransaction = 0;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            //MoneyInTransaction = 0;
        }
    }
}

