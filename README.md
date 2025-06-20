# FoodCreator
Prosty projekt demonstracyjny aplikacji typu frontend/backend. Backend napisany w .NET 8 przy użyciu Entity Framework, frontend w React.js oraz lokalna baza danych SQL Server.

## Funkcje
- Interfejs użytkownika w React.js
- Serwer wykonany przy użyciu Entity Framework oraz baza danych w SQL Server
- Integracja z Firebase przy autentykacji użytkowników
- Możliwość dodawania dań oraz własnych składników wraz ze zdjęciami
- Obsługa CRUD

# 1. Uruchomienie:  
`docker-compose up`

Konteneryzacja pozwala na zintegrowanie backendu z frontendem w prosty i przejrzysty sposób, dzięki czemu wiele osób może pracować na kodzie aplikacji przy zachowaniu każdorazowo tej samej konfiguracji. Frontend będzie dostępny pod http://localhost:5000, backend pod http://localhost:3000

## Struktura
- backend/ – aplikacja ASP.NET napisana przy użyciu Entity Framework zintegrowana z lokalną bazą danych SQL Server
- frontend/ – aplikacja React.js z interfejsem użytkownika, formularzami oraz logowaniem

## Technologie
- Frontend:
    - React.js
    - Firebase
- Backend:
    - .NET 8 
    - Entity Framework
    - SQL Server
- Konteneryzacja:
    - Docker
    - Docker Compose
