using System;
namespace DddInPractice.Logic
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
        public int QuarterCentCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
                    OneCentCount * 0.01m +
                    TenCentCount * 0.10m +
                    QuarterCentCount * 0.25m +
                    OneDollarCount +
                    FiveDollarCount * 5 +
                    TwentyDollarCount * 20;

        public Money(int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
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
            QuarterCentCount = quarterCentCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }


        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCentCount + money2.QuarterCentCount,
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
                money1.QuarterCentCount - money2.QuarterCentCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
            return sum;
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCentCount == other.QuarterCentCount
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
                hashcode = (hashcode * 397) ^ QuarterCentCount;
                hashcode = (hashcode * 397) ^ OneDollarCount;
                hashcode = (hashcode * 397) ^ FiveDollarCount;
                hashcode = (hashcode * 397) ^ TwentyDollarCount;
                return hashcode;
            }
        }
    }
}

