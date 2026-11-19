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
}