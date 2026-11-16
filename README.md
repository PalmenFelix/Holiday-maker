## Instruktioner

För att starta programmet behöver man först skapa en databas i MySQL. Sen behöver man öppna filen SQL/holiday_maker.sql och köra skriptet. Då skapas tabellerna och testdatan som behövs. Databasen körs med hjälp av Docker.

Innan man sen kan köra programmet behöver man skriva in databasens lösenord, det gör man genom att skriva: export DB_PASSWORD="lösenordet". Lösenordet är det som stor i artikeln "Installera MySQL i Docker"

Sen startar man programmet genom att skriva "dotnet run" i terminalen. Då kommer huvudmenyn upp och man kan välja vilken funktion man vill använda.
