namespace ATM.Tests;

using Xunit;

public class CardTest
{
    private Card card = new Card("1234-5678", "1234", new Account(5000));

    [Fact]
    public void MatchesPin_CorrectPin_ReturnsTrue()
    {
        bool result = card.MatchesPin("1234");

        Assert.True(result);
    }

    [Fact]
    public void MatchesPin_WrongPin_ReturnFalse()
    {
        bool result = card.MatchesPin("1111");

        Assert.False(result);
    }

    [Theory]
    [InlineData("2345")]
    [InlineData("123")]
    [InlineData("12345")]
    [InlineData("")]

    public void MatchesPin_InvalidPins_ReturnFalse(string pin)
    {
        bool result = card.MatchesPin(pin);

        Assert.False(result);
    }



}