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
        public virtual decimal MoneyInTransaction { get; protected set; } 

        protected virtual IList<Slot> Slots { get;  set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = 0; 
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

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public virtual void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0;
        }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
        public virtual void  BuySnack(int position)
        {
            Slot slot =GetSlot(position);

            if (slot.SnackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();
            
            slot.SnackPile = slot.SnackPile.SubstractOne();

            MoneyInTransaction = 0;
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