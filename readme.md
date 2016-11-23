# Setup
## benötigte Software
- Visual Studio 2015 Community (https://www.visualstudio.com/de/downloads/)
- beim Installieren darauf achten, dass Windows- und WebentwicklerTools > Microsoft Web Developer Tools installiert wird
- ansonsten: über Programme > Microsoft Visual Studio > Ändern nachinstallieren lassen

## Restore Packages
Wenn das Projekt erstmals heruntergeladen wurde: 
- öffne es in Visual Studio (oder vergleichbare IDE), indem die .sln Datei doppelgeklickt wird
- gehe im Solution Explorer zu References (-> Nuget Manager sollte sich öffnen)
- Klicke auf restore
- => fehlende Pakete werden installiert

## Mail Provider zufügen
- der Email Service ist auf einen gmail Account konfiguriert
- Trage Logindaten (zum Test) unter App_Start > IdentityConfig.cs in fromMail und mailPw ein
- besser: externe setup datei, erstellung erklärt: TODO
