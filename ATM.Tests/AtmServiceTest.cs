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


}