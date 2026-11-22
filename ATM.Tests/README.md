# ATM26

# ATM26 – Enhetstester med xUnit

Övning i kursen **Test, integration, leverans** (MAI26HA).
En bankomat i C# som jag har testat och förbättrat med testdriven utveckling (TDD).

## Köra projektet

- Kör testerna: `dotnet test ATM.Tests/ATM.Tests.csproj`
- Kör programmet: `dotnet run --project ATM`

## Tester (27 st)

| Testklass | Vad som testas |
|---|---|
| `AccountTest` | Konstruktor, insättning, uttag, gränsvärden (uttag av exakt hela saldot), nekade belopp (noll, negativa, mer än saldot) |
| `CardTest` | Rätt PIN godkänns, fel PIN nekas, flera ogiltiga PIN (`[Theory]`: för kort, för lång, tom) |
| `AtmServiceTest` | Sätta in och mata ut kort, rätt och fel PIN, saldo, insättning, uttag (5000 / 6000 / 8000), säkerhet utan PIN, 3 PIN-försök, utloggning vid utmatning |

## Buggar jag hittade och fixade

1. **`Account.Deposit`** – det gick att sätta in noll eller negativa belopp. En insättning på −500 minskade saldot.
2. **`Account.Withdraw`** – det gick att ta ut mer än saldot och negativa belopp. Kontot kunde hamna på minus.
3. **`AtmService.Withdraw` (ordningen)** – bankomaten drog sedlar från sig själv *innan* kontot hade svarat. Vid nekat uttag (6000 från ett konto med 5000) försvann ändå 6000 från bankomatens saldo.
4. **`AtmService.Withdraw` (bankomatens sedlar)** – bankomaten kontrollerade aldrig om den hade tillräckligt med kontanter. Ett uttag på 8000 när maskinen bara hade 7000 gick igenom och bankomaten hamnade på −1000.

Varje bugg hittades med ett test som först var **rött**, och fixades sedan tills testet blev **grönt**. Det syns i commit-historiken (`Test: ... (rött)` följt av `Fix: ... (grönt)`).

## Förbättringar

- **Enum `WithdrawResult`** (`Success`, `InsufficientFunds`, `InsufficientAtmCash`) – `Withdraw` svarade tidigare bara `true`/`false`, så kunden kunde inte få veta *varför* ett uttag nekades. Nu visar menyn rätt meddelande, och testerna kan kontrollera exakt vilken orsak det blev. Det gjorde koden mer **testbar**.
- **3 PIN-försök** – kortet behålls efter tre felaktiga PIN-koder i rad. Logiken ligger i `AtmService` och är testad. Menyn frågar bara bankomaten om kortet finns kvar.
- **`int.TryParse` i menyn** – programmet kraschade om man skrev bokstäver som belopp. Nu visas "Ogiltigt belopp" i stället. Detta tog också bort två kompilatorvarningar (CS8604).

## Säkerhetstester

Tester som bevisar att saldo, uttag och insättning **stoppas** när kunden inte är inloggad (`Assert.Throws<InvalidOperationException>`), och att kunden **loggas ut** när kortet matas ut. Skyddet fanns redan i koden – testerna bekräftar att det fungerar och att det inte går sönder vid framtida ändringar.

## Manuell testning

Med `dotnet run` hittade jag två problem i menyn som enhetstesterna inte kunde hitta, eftersom de testar `AtmService` och inte menyn:

1. Menyn matade ut kortet redan efter **första** felaktiga PIN, trots att `AtmService` stödde tre försök.
2. Efter första fixen skrev menyn fortfarande ut "Kortet matas ut" fast kortet låg kvar.

Båda är fixade. Det visar varför både **automatiserad** och **manuell** testning behövs.

## Verifiering och validering

- **Verifiering** (följer koden kraven?) – de automatiska xUnit-testerna, t.ex. att uttag av 6000 nekas enligt uppgiften.
- **Validering** (fungerar det för en riktig användare?) – manuell testning i menyn, t.ex. att det kändes fel att kortet matades ut efter ett enda PIN-fel.

## Medvetet bortvalt

Uppgiftens idélista innehåller över 30 möjliga funktioner. Jag valde bort bland annat flera valutor, växelkurser, språkval, kvitton, transaktionshistorik och hela tillståndsmaskinen. Anledningen är att de är stora i förhållande till nyttan i en övning, och att kursplanen betonar att **antal tester eller funktioner inte i sig ger högre betyg** – det viktiga är relevanta tester och att kunna motivera sina val.

## Möjliga förbättringar

- Insättning ökar idag bara kontots saldo, inte bankomatens sedlar. I