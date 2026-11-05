
# Матриця трасованості вимог Class Schedule System

## 1. Призначення

Матриця трасованості встановлює зв'язок між функціональними та нефункціональними вимогами, варіантами використання, архітектурними компонентами та UML-діаграмами.

## 2. Матриця трасованості

| ID вимоги | Опис вимоги | Use Case | Архітектурний компонент | UML-діаграма |
|---|---|---|---|---|
| FR-01 | Перегляд розкладу групи | UC-01 | Web Interface, Schedule Controller | Use Case, Component |
| FR-02 | Перегляд розкладу викладача | UC-02 | Web Interface, Schedule Controller | Use Case, Component |
| FR-03 | Створення розкладу | UC-03 | Schedule Controller, Schedule Service | Use Case, Sequence |
| FR-04 | Редагування розкладу | UC-04 | Schedule Controller, Schedule Service | Use Case, Component |
| FR-05 | Видалення занять | UC-05 | Schedule Controller, Schedule Service | Use Case, Component |
| FR-06 | Перевірка конфліктів | UC-03 | Conflict Checker | Sequence, Component |
| FR-07 | Перевірка коректності розкладу | UC-03 | Validation Service | Sequence, Component |
| FR-08 | Керування користувачами | UC-06 | User Controller, User Service | Use Case, Component |
| NFR-01 | Продуктивність | Усі | Schedule Service, Repository | Component |
| NFR-02 | Безпека | Усі | User Service | Component |
| NFR-03 | Надійність | Усі | Schedule Repository, Database | Component |
| NFR-04 | Зручність використання | Усі | Web Interface | Component |

## 3. Зв'язок архітектурних рішень із вимогами

| Архітектурне рішення | Пов'язані вимоги |
|---|---|
| ADR-001: Багаторівнева архітектура | NFR-01, NFR-04 |
| ADR-002: Реляційна база даних | NFR-03, FR-01, FR-03, FR-04 |

## 4. Висновок

Матриця трасованості забезпечує простежуваність вимог від їх визначення до архітектурної реалізації. Вона допомагає перевірити, чи всі основні вимоги враховані під час проєктування системи Class Schedule System.
