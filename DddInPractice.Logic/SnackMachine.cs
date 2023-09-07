using System;
namespace DddInPractice.Logic
{
    public sealed class SnackMachine
    {
        public int OneCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCentCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }


        public int OneCentCountInTransaction { get;private set; }
        public int TenCentCountInTransaction { get;private set; }
        public int QuarterCentCountInTransaction { get;private set; }
        public int OneDollarCountInTransaction { get;private set; }
        public int FiveDollarCountInTransaction { get;private set; }
        public int TwentyDollarCountInTransaction { get;private set; }

        public void InsertMoney(int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount)
        {
            OneCentCountInTransaction += oneCentCount;
            TenCentCountInTransaction += tenCentCount;
            QuarterCentCountInTransaction += quarterCentCount;
            OneDollarCountInTransaction += oneDollarCount;
            FiveDollarCountInTransaction += fiveDollarCount;
            TwentyDollarCountInTransaction += twentyDollarCount;
        }

        public void ReturnMoney()
        {
            OneCentCountInTransaction = 0;
            TenCentCountInTransaction = 0;
            QuarterCentCountInTransaction = 0;
            OneDollarCountInTransaction = 0;
            FiveDollarCountInTransaction = 0;
            TwentyDollarCountInTransaction = 0;
        }

        public void BuySnack()
        {
            OneCentCount += OneCentCountInTransaction;
            TenCentCount += TenCentCountInTransaction;
            QuarterCentCount += QuarterCentCountInTransaction;
            OneDollarCount += OneDollarCountInTransaction;
            FiveDollarCount += FiveDollarCountInTransaction;
            TwentyDollarCount += TwentyDollarCountInTransaction;

            OneCentCountInTransaction = 0;
            TenCentCountInTransaction = 0;
            QuarterCentCountInTransaction = 0;
            OneDollarCountInTransaction = 0;
            FiveDollarCountInTransaction = 0;
            TwentyDollarCountInTransaction = 0;
        }
    }
}

