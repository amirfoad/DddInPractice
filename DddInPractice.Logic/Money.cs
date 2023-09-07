using System;
namespace DddInPractice.Logic
{
	public sealed class Money :ValueObject<Money>
	{
        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCentCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }
        public Money(int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCentCount = quarterCentCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }


        public static Money operator +(Money money1,Money money2)
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

