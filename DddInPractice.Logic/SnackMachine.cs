using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic
{
    public class SnackMachine : Entity
    {
        /// <summary>
        /// The amount of money machine has
        /// </summary>
        public virtual Money MoneyInside { get; protected set; }

        /// <summary>
        /// The amount of money user inserted
        /// </summary>
        public virtual Money MoneyInTransaction { get; protected set; } 

        public virtual IList<Slot> Slots { get; protected set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = None; 
            Slots = new List<Slot>()
            {
                new Slot(null,0,0m,this,1),
                new Slot(null,0,0m,this,2),
                new Slot(null,0,0m,this,3),
            };
        }

        public virtual void InsertMoney(Money money)
        {
            Money[] coinAndNotes =
            {
                Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar
            };
            if (!coinAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public virtual void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public virtual void  BuySnack(int position)
        {
            Slot slot = Slots.Single(s => s.Position == position);
            slot.Quantity--;
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }

        public virtual void LoadSnacks(int position,
            Snack snack,
            int quantity,
            decimal price)
        {
            Slot slot = Slots.Single(snack => snack.Position == position);
            slot.Snack = snack;
            slot.Quantity = quantity;
            slot.Price = price;
        }
    }
}