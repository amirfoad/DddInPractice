using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.SharedKernel
{
    public sealed class Money : ValueObject<Money>
    {
        public static readonly Money None = new(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
                    OneCentCount * 0.01m +
                    TenCentCount * 0.10m +
                    QuarterCount * 0.25m +
                    OneDollarCount +
                    FiveDollarCount * 5 +
                    TwentyDollarCount * 20;

        private Money()
        { }

        public Money(int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) : this()
        {
            if (oneCentCount < 0
                || tenCentCount < 0
                || quarterCentCount < 0
                || oneDollarCount < 0
                || fiveDollarCount < 0
                || twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCentCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator *(Money money,int multiplier)
        {
            Money sum = new(
                money.OneCentCount * multiplier,
                money.TenCentCount * multiplier,
                money.QuarterCount * multiplier,
                money.OneDollarCount * multiplier,
                money.FiveDollarCount * multiplier,
                money.TwentyDollarCount * multiplier);
            return sum;
        }
        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);
            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            Money sum = new(
                money1.OneCentCount - money2.OneCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
            return sum;
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCount == other.QuarterCount
                && OneDollarCount == other.OneDollarCount
                && FiveDollarCount == other.FiveDollarCount
                && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            //unchecked used to suppress overflow-checking for integral-type arithmetic operations and conversions
            unchecked
            {
                int hashcode = OneCentCount;
                hashcode = (hashcode * 397) ^ TenCentCount;
                hashcode = (hashcode * 397) ^ QuarterCount;
                hashcode = (hashcode * 397) ^ OneDollarCount;
                hashcode = (hashcode * 397) ^ FiveDollarCount;
                hashcode = (hashcode * 397) ^ TwentyDollarCount;
                return hashcode;
            }
        }

        public override string ToString()
        {
            if (Amount < 1)
                return "¢" + (Amount * 100).ToString("0");

            return "$" + Amount.ToString("0.00");
        }

        public Money Allocate(decimal amount)
        {
            if (!CanAllocate(amount))
                throw new InvalidOperationException();

            return AllocateCore(amount);
        }
        public Money AllocateCore(decimal amount)
        {
            int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
            amount = amount - twentyDollarCount * 20;
            
            int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount = amount - fiveDollarCount * 5;
            
            int oneDollarCount = Math.Min((int)(amount), OneDollarCount);
            amount = amount - oneDollarCount;
            
            
            int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCount);
            amount = amount - quarterCount * 0.25m;
            
            int tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
            amount = amount - tenCentCount * 0.1m;
            
            int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);


            return new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);
        }

        public bool CanAllocate(decimal amount)
        {
            Money money = AllocateCore(amount);
            return money.Amount == amount;
        }
    }
}