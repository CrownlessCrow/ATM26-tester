namespace ATM.Tests;

using Xunit;

public class AtmServiceTest

{
    private Account account;
    private Card card;
    private AtmService atm;

    public AtmServiceTest()

    {
        account = new Account(5000);
        card = new Card("1234-5678", "1234", account);
        atm = new AtmService(7000);
    }

    [Fact]
    public void InsertCard_AtmKnowsCardIsInserted()

    {
        atm.InsertCard(card);
        Assert.True(atm.HasCardInserted);

    }
    [Fact]

    public void EnterPin_WrongPin_IsRejected()

    {
        atm.InsertCard(card);
        bool resultat = atm.EnterPin("1111");

        Assert.False(resultat);
        Assert.False(atm.IsAuthenticated);
    }
    [Fact]
    public void EnterPin_CorrectPin_IsAuthenticated()
    {
        atm.InsertCard(card);
        bool resultat = atm.EnterPin("1234");

        Assert.True(resultat);
        Assert.True(atm.IsAuthenticated);
    }

    [Fact]
    public void EjectCard_AtmHasNoCard()
    {
        atm.InsertCard(card);
        atm.EjectCard();

        Assert.False(atm.HasCardInserted);
    }
    [Fact]
    public void GetBalance_WhenLoggedIn_ReturnsAccountBalance()
    {
        atm.InsertCard(card);
        atm.EnterPin("1234");

        int saldo = atm.GetBalance();

        Assert.Equal(5000, saldo);
    }
    [Fact]
    public void Deposit_WhenLoggedIn_IncreasesAccountBalance()

    {
        atm.InsertCard(card);
        atm.EnterPin("1234");

        bool resultat = atm.Deposit(1000);


        Assert.True(resultat);
        Assert.Equal(6000, atm.GetBalance());
    }

}