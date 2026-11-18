namespace ATM.Tests;

using System.IO.Pipelines;
using Xunit;

public class AccountTest

{
    private Account account = new Account(5000);

    [Fact]
    public void Constructor_SetsInitialBalance()

    {
        Assert.Equal(5000, account.GetBalance());
    }

    [Fact]
    public void Deposit_IncreasesBalance()

    {
        bool result = account.Deposit(1000);

        Assert.True(result);
        Assert.Equal(6000, account.GetBalance());
    }

    [Fact]

    public void Deposit_NegativeAmount_IsRejected()

    {
        bool result = account.Deposit(-500);

        Assert.False(result);
        Assert.Equal(5000, account.GetBalance());
    }

    [Fact]

    public void Withdraw_DecreasesBalance()
    {
        bool result = account.Withdraw(1000);


        Assert.True(result);
        Assert.Equal(4000, account.GetBalance());
    }

    [Fact]
    public void Withdraw_MoreThanBalance_IsRejected()

    {
        bool result = account.Withdraw(6000);

        Assert.False(result);
        Assert.Equal(5000, account.GetBalance());
    }

    [Fact]
    public void Withdraw_NegativeAmmount_IsRejected()
    {
        bool result = account.Withdraw(-500);

        Assert.False(result);
        Assert.Equal(5000, account.GetBalance());

    }
    [Fact]
    public void Withdraw_ExactBalance_IsAllowed()

    {
        bool result = account.Withdraw(5000);

        Assert.True(result);
        Assert.Equal(0, account.GetBalance());
    }

}