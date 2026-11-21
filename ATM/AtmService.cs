namespace ATM;

public class AtmService
{
    private Card? _currentCard;
    private bool _isAuthenticated;
    private int _failedAttempts;

    public bool HasCardInserted => _currentCard != null;
    public bool IsAuthenticated => _isAuthenticated;

    public int AtmBalance { get; private set; }

    public AtmService(int initialBalance)
    {
        AtmBalance = initialBalance;
    }

    public void InsertCard(Card card)
    {
        _currentCard = card;
        _isAuthenticated = false;
        _failedAttempts = 0;
    }

    public void EjectCard()
    {
        _currentCard = null;
        _isAuthenticated = false;
    }

    public bool EnterPin(string pinCode)
    {
        if (_currentCard == null) //Finns det ett kort i?
        {
            return false;
        }

        _isAuthenticated = _currentCard.MatchesPin(pinCode); //Fråga kortet om Pin koden stämmer + att vi är inloggad
        //return _isAuthenticated; flyyta ner

        if (!_isAuthenticated)
        {
            _failedAttempts++;

            if (_failedAttempts >= 3)
            {
                _currentCard = null;
            }

        }
        return _isAuthenticated;
    }

    public int GetBalance()
    {
        EnsureAuthenticated();
        return _currentCard!.Account.GetBalance();
    }


    public WithdrawResult Withdraw(int amount)
    //public bool Withdraw(int amount)
    {
        EnsureAuthenticated();

        if (amount > AtmBalance)
        {
            return WithdrawResult.InsufficientAtmCash;
            //return false;
        }

        bool kontotSaja = _currentCard!.Account.Withdraw(amount);
        if (!kontotSaja)

        {
            return WithdrawResult.InsufficientFunds;
            //return false;
        }

        AtmBalance -= amount;
        return WithdrawResult.Success;
        //return true;
    }

    public bool Deposit(int amount)
    {
        EnsureAuthenticated();
        return _currentCard!.Account.Deposit(amount);
    }

    public void EnsureAuthenticated() //Inget kort  eller inte Inloggad
    {
        if (_currentCard == null || !_isAuthenticated)
        {
            throw new InvalidOperationException("Ingen autentiserad session.");
        }
    }
}