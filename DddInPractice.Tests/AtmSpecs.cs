using DddInPractice.Logic;
using DddInPractice.Logic.Atms;
using DddInPractice.Logic.SharedKernel;
using FluentAssertions;
using static DddInPractice.Logic.SharedKernel.Money;
namespace DddInPractice.Tests;

public class AtmSpecs
{
    [Fact]
    public void Take_money_exchanges_money_with_commission()
    {
        Atm atm = new();
        atm.LoadMoney(Dollar);
        
        atm.TakeMoney(1m);

        atm.MoneyInside.Amount.Should().Be(0m);
        atm.MoneyCharged.Should().Be(1.01m); 
    }

    [Fact]
    public void Commission_is_at_least_one_cent()
    {
        Atm atm = new();
        atm.LoadMoney(Cent);
        
        atm.TakeMoney(0.01m);

        atm.MoneyCharged.Should().Be(0.02m);
    }

    [Fact]
    public void Commission_is_rounded_up_to_the_next_cent()
    {
        Atm atm = new();
        atm.LoadMoney(Dollar + TenCent);
        
        atm.TakeMoney(1.1m);

        atm.MoneyCharged.Should().Be(1.12m);
    }
}