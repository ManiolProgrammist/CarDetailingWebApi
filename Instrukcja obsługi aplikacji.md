# Instrukcja obsługi aplikacji

# 1 stworzenie bazy danych
Należy stworzyć bazę danych sql, skrypt potrzebny do jej stworzenia znajduje się w pliku "SQL DB" w folderze "CarDetailingDB".

# 2 Instalacja oraz uruchomienie aplikacji serwerowej

# IIS Manager
Aby uruchomić aplikację serwerową należy zainstalować IIS Manager
W panelu sterowania należy wejść w obszar "Programy i funkcję". W obszarze Programy i funkcje należy kliknąć opcję Włącz lub wyłącz funkcje systemu Windows. 

W okienku Funkcji systemu windows należy zlokalizować Internetowe usługi informacyjne (Internet Information Services) i kliknąć w pole wyboru obok niego. 
Spowoduje to sprawdzenie domyślnych funkcji podrzędnych. 

Następnie należy rozwinąć opcję Usługi WWW (World Wide Web Services), po czym rozwinąć opcję Funkcje tworzenia aplikacji (Application Develompnent Features). 
Należy zaznaczyć wszystkie elementy znajdujące się pod opcją Funkcje tworzenia aplikacji (Application Develompnent Features) 
Po wykonaniu powyższych instrukcji należy zamknąć okno przyciskiem "Ok".

# Uruchomienie aplikacji klienckiej

Aby uruchomić aplikację serwerową nalezy uruchomić Internet Information Services (IIS) Manager. 
W rozwijanej liście po lewej stronie należy rozwinąć element z nazwą aktualnego użytkownika. Po rozwinięciu ukażą się dwa elementy "Application Pools" oraz "Sites". 
Następnie należy kliknąć prawym przyciskiem myszy na pole "Application Pools" oraz wybrać opcję "Add Application Pool". 
W okienku które się pokaże należy wpisać nazwę "CarDetailing", w polu .Net CLI Version należy wybrać opcję .NET CLR Version v4.0.30319, 
w polu managed pipeline mode należy wybrać "Integrated" oraz zaznaczyć opcję "Start application pool immadiately"
Po wykonaniu tych operacji należy wybrać "Ok" aby zamknąć okienko.
Następnie należy kliknąć prawym przyciskiem myszy na pole "Sites" oraz wybrać opcję "Add Website".
W polu Site name należy wpisać "CarDetailing", następnie należy wybrać przyciskiem "Select..." opcję "CarDetailing". 
W Polu "Physical path" należy podać ścieżkę do folderu części praktycznej "CarDetailingWebApi".
Następnie w polu "Port" należy wpisać port 51547. Należy kliknąć w przycisk "Connect as...", wybrać opcję "specyfic user" oraz wpisać dane potrzebne do logowania użytkownika stacji roboczej (zalogowanego użytkownika) oraz kliknąć "ok".

Następnie należy rozwinąć opcję "Sites" oraz wybrać stworzoną właśnie stronę "CarDetailing" i upewnić się że z prawej strony pod "Manage Website" serwer jest uruchomiony. Następnie należy wybrać opcję "Browse Website".

# 3 Instalacja oraz uruchomienie aplikacji klienta

Do uruchomienia aplikacji klienta niezbędny jest framework Node.js w wersji 12.9.1 lub wyższej, dostępny pod adresem https://nodejs.org/download/release/v12.9.1/
Po zainstalowaniu wspomnianego frameworka należy przy pomocy modułu npm zainstalować bibliotekę @angular/cli. W tym celu należy uruchomić konsolę cmd systemu Windows i w oknie tej konsoli wpisać komendę 
> npm install -g @angular/cli. 

Komenda ta instaluje globalnie bibliotekę CLI frameworka Angular.
W celu uruchomienia aplikacji klienta, należy przejść do folderu projektu o nazwie CarDetailingAngular. 
Celem zmniejszenia rozmiaru projektu, usunięty został folder node_modules, który przechowuje wszystkie pliki bibliotek użytych do stworzenia projektu. 
Aby aplikacja działała poprawnie, należy te biblioteki zainstalować przy użyciu komendy
> npm install

Do instalacji bibliotek potrzebna będzie konsola cmd systemu Windows, której ścieżka jest ustawiona na folder CarDetailingAngular.


Gdy biblioteki zostaną prawidłowo zainstalowane, do projektu zostanie dodany folder node_modules. Po wykonaniu wyżej wspomnianych operacji, 
możliwe jest uruchomienie aplikacji klienta. Aby to zrobić, należy w konsoli cmd systemu Windows, 
której ścieżka jest ustawiona na folder CarDetailingAngular, użyć komendy 
> ng serve -o

Po jej użyciu, aplikacja zostanie uruchomiona lokalnie w domyślnej przeglądarce przypisanej do użytkownika systemu Windows. W przypadku nieuruchomienia aplikacji w przeglądarce, 
należy uruchomić przeglądarkę Google Chrome lub Mozilla Firefox, a następnie w oknie adresu URL wpisać adres http://localhost:4200/.

Po uruchomieniu aplikacji serwerowej oraz klienckiej, pierwszy zarejestrowany użytkownik otrzyma prawa administratora.

