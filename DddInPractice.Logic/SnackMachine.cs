using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic
{
    public class SnackMachine : Entity
    {
        /// <summary>
        /// The amount of money machine has
        /// </summary>
        public virtual Money MoneyInside { get; protected set; } = None;

        /// <summary>
        /// The amount of money user inserted
        /// </summary>
        public virtual Money MoneyInTransaction { get; protected set; } = None;

        public virtual void InsertMoney(Money money)
        {
            Money[] coinAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!coinAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public virtual void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }
    }
}