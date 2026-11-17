namespace ATM.Tests;

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



}