## Individuell reflektion

Jag tycker datamodellen fungerar bra. Jag valde att dela upp all information mellan fyra tabeller för att hotell, rum, kunder och bokningar innehåller olika typer av information. Det gör databasen simplare och gör så att av information inte lagras på samma ställe. Hotel, Rooms, Customer och Booking är även alla kopplade till varandra med primärnycklar och främmande nycklar. Det gör så att datan för de olika taballerna faktiskt hänger ihop bra. Jag valde också att SQL skulle kontrollera om ett rum redan var bokat. Så att programmet filtrera bort upptagna rum innan kunden bokar.

Säkerhet är inget jag har lagt särskilt stort fokus på men det finns ändå några saker som gör databasen och programmet säkrare. Det finns begränsningar i databasen, till exempel för att kontrollera datum och antal personer. Dem begränsningarna finns så att ogiltliga värden, t.ex. ogiltiga datum eller för många personer i ett rum inte ska kunna sparas i databasen. Ett databaslösenord krävs också för att kunna ansluta till databasen och starta programmet.

Om jag skulle haft mer tid till att kunna förbättra programmet så hade jag t.ex. utvecklat sökfunktionen med fler filter och val. Jag hade även kunnat förbättra felhanteringen och göra menyerna tydligare och simplare att använda.

Jag kontrollerade resultatet genom att köra programmet i VS Code och testa massa olika val i menyn. Jag kontrollerade även i DBeavers SQL console så att saker stämde. Jag använde AI först till att först hjälpa mig planera projektet. Sen under arbetsprocessen när jag fastnade på saker i koden, hade problem med syntaxen eller för att felsöka. 

