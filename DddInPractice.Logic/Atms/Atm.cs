using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using static DddInPractice.Logic.SharedKernel.Money;
namespace DddInPractice.Logic.Atms;

public class Atm:AggregateRoot
{
    private const decimal CommissionRate = 0.01m;
    public virtual Money MoneyInside { get; protected set; }=None;
    public virtual decimal MoneyCharged { get; protected set; }


    public virtual string CanTakeMoney(decimal amount)
    {
        if (amount <= 0m)
            return "Invalid amount";

        if (MoneyInside.Amount < amount)
            return "Not enough money";

        if(!MoneyInside.CanAllocate(amount))
            return "Not enough change";
        
        return string.Empty;
    }
    public virtual void TakeMoney(decimal amount)
    {
        if (!string.IsNullOrEmpty(CanTakeMoney(amount)))
            throw new InvalidOperationException();
        
        Money output = MoneyInside.Allocate(amount);
        MoneyInside -= output;

        decimal amountWithCommission = CalculateAmountWithCommision(amount);
        MoneyCharged += amountWithCommission;
    }

    private decimal CalculateAmountWithCommision(decimal amount)
    {
        decimal commision = amount * CommissionRate;
        decimal lessThanCent = commision % 0.01m;
        if (lessThanCent > 0)
            commision = commision - lessThanCent + 0.01m;

        return amount + commision;
    }
    
    
    public virtual void LoadMoney(Money money)
    {
        MoneyInside += money;
    }
}