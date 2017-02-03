# Setup
## benötigte Software
- Visual Studio 2015 Community (https://www.visualstudio.com/de/downloads/)
- beim Installieren darauf achten, dass Windows- und WebentwicklerTools > Microsoft Web Developer Tools installiert wird
- ansonsten: über Programme > Microsoft Visual Studio > Ändern nachinstallieren lassen

## Restore Packages
Wenn das Projekt erstmals heruntergeladen wurde: 
- öffne es in Visual Studio (oder vergleichbare IDE), indem die .sln Datei doppelgeklickt wird
- Starte Projekt
- => fehlende Pakete werden installiert

## Mail Provider zufügen
- Es ist KEIN MAIL PROVIDER im GitHub konfiguriert, um die Zugangsdaten zu schützen
- Momentan ist der Email Service auf einen gmail Account konfiguriert

Um einen eigenen MailService einzubinden:
- Trage Logindaten unter App_Start > IdentityConfig.cs in fromMail und mailPw ein
- Erfreuen Sie sich an Confirmation Mails & 2 Factor Authentification.

## Datenbank erstellen
Vor dem ersten Start des Projektes die "Package Manager Console" (Tools>Nuget Packet Manage>Package Manager Console) öffnen und 'Update-Database' eingeben

Falls es zu folgendem Fehler kommt: Cannot attach the file *.mdf as database

- öffne die Package Manager Console
- gebe 'sqllocaldb info' ein
- der Eintrag 'MSSQLLocalDB' sollte angezeigt werden
- lösche die Datenbank mittels 'sqllocaldb stop MSSQLLocalDB' & 'sqllocaldb delete MSSQLLocalDB'
- gebe 'Update-Database' ein
