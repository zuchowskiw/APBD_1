# APBD_1

Projekt wprowadza funkcjonalność wypożyczalni sprzętu poprzez CLI. Aktualnie zaimplementowane zostały 3 typy sprzętu: 
  - Laptop
  - Projector
  - Camera

oraz 2 typy użytkowników:
  - Student
  - Employee

Zdefiniowane zostały 3 klasy z ekstensjami, które odpowiadają za (de)serializację danych: 
  - Rental - metadane dotyczące wypożyczeń sprzętu
  - User   - dane użytkowników
  - Devices - parent klasa wszystkich urządzeń

Klasy te nie przekazują sobie referencji instancji; zamiast tego posługują się unikatowymi ID dla tych obiektów, aby szukać 
ich w ekstensji. Zostało to zrobione w celu decouplingu oraz łatwiejszej logiki serializacji.

Service został zaimplementowany jako klasa zajmująca się interfejsem z konsolą - tłumaczy komendy z argumentów na funkcje opisane w wymaganej funkcjonalności zadania.
