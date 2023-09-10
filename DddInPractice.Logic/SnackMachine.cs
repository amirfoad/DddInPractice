using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        /// <summary>
        /// The amount of money machine has
        /// </summary>
        public virtual Money MoneyInside { get; protected set; }

        /// <summary>
        /// The amount of money user inserted
        /// </summary>
        public virtual Money MoneyInTransaction { get; protected set; } 

        protected virtual IList<Slot> Slots { get;  set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = None; 
            Slots = new List<Slot>()
            {
                new Slot(null,1),
                new Slot(null,2),
                new Slot(null,3 ),
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
            Slot slot =GetSlot(position);

            if (slot.SnackPile.Price > MoneyInTransaction.Amount)
                throw new InvalidOperationException();
            
            slot.SnackPile = slot.SnackPile.SubstractOne(); 
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }

        public virtual void LoadSnacks(int position,
            SnackPile snackPile)
        {
            Slot slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        public virtual SnackPile GetSnackPile(int position) => GetSlot(position).SnackPile;

        private Slot GetSlot(int position) => Slots.Single(s => s.Position == position);
    }
}