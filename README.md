# Employee Management System

## Описание
**Employee Management System** — это приложение для учета сотрудников компании. Оно позволяет добавлять, редактировать, удалять сотрудников, учитывать отпуска и больничные, а также формировать отчеты и осуществлять поиск по различным критериям.

---

## Технологии
### Клиентская часть:
- **WPF**: используется для создания интерфейса пользователя.
- **MVVM**: архитектурный шаблон для разделения логики приложения и пользовательского интерфейса.
- **ReactiveUI**: библиотека для реактивного программирования, упрощает работу с MVVM.
- **SPA (Single Page Application)**: концепция построения интерфейса, где взаимодействие с сервером минимизируется.

### Серверная часть:
- **ADO.NET**: используется для подключения к базе данных и выполнения запросов.
- **PostgreSQL**: реляционная база данных для хранения данных о сотрудниках, отделах и должностях.

### Логгирование:
- **N.Log**: используется для удобства поиска и быстрого исправления ошибок

---

## Функционал
- Добавление, редактирование и удаление сотрудников.
- Учет отпусков и больничных.
- Формирование отчетов.
- Поиск сотрудников по критериям (например, по отделу или должности).

---

## Структура проекта
### Client
Содержит клиентскую часть приложения:
- **Client.GUI**: реализует графический интерфейс пользователя с использованием WPF и MVVM.
- **Client.HTTP**: отвечает за взаимодействие клиента с серверной частью через HTTP-запросы.

### Server
Содержит серверную часть приложения:
- **Server.API**: предоставляет REST API для обработки запросов от клиента.
- **Server.BL (Business Logic)**: слой бизнес-логики, где реализуются основные правила работы приложения.
- **Server.DAL (Data Access Layer)**: отвечает за взаимодействие с базой данных PostgreSQL через ADO.NET.

### Models
Содержит общие модели данных, используемые как на клиенте, так и на сервере.

=============================================================================
## Оформление и правила написания кода

Для обеспечения читаемости и согласованности кода в проекте необходимо придерживаться следующих правил:

1. **Приватные переменные:**
   - Названия приватных переменных начинаются с символа `_` (подчеркивания).
   - Пример: `_privateVariable`.

2. **Составные переменные:**
   - Для составных имен используется стиль `CamelCase`, начиная с маленькой буквы.
   - Пример: `namePerson`, `employeeCount`.

3. **Асинхронные методы:**
   - Названия асинхронных методов должны заканчиваться суффиксом `Async`.
   - Пример: `LoadDataAsync`, `GetEmployeeListAsync`.

4. **Смысловые названия:**
   - Названия методов и переменных должны четко передавать их суть или описывать выполняемую работу.
   - Пример:
     - Метод `CalculateSalaryAsync` — для расчета зарплаты.
     - Переменная `employeeList` — для списка сотрудников.
5. **Именование классов:**
   - В зависимости от слоя и модели данных (Например: EmployeeService или PersonDTO)
      
7. **Обработка исключений:**
   - Используете логгер для в тех местах где могут быть потенциалные ошибки.
   - Во всех слоях уже подключен N.Log
   - Чтобы он работал, реализуйте в классе в котором хотите его использовать:
   - private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
     А также блоки try - catch.
     
7.**Работа с Git:**
  - Старайтесь каждый логический блок кода коммитить и правильно описывать
  - Новая задача => новая ветка с последующим слияним с главной,при успешном тестировании

8.**Использование отступов для читаемости кода:**
  - Отступы в один Tab

8. **Принцип единственной ответственности:**
   - Класс должен выполнять только одну задачу и иметь только одну причину для изменения. Это правило также применимо к методам: каждый метод должен быть небольшим и выполнять только одну логическую задачу. Если метод делает несколько вещей, его следует разделить на          более мелкие методы.
  
====================================================================================
# Реализация слоёв: 
 - БД и DAL  - Артем 
 - BL и API  - Сергей и Дмитрии
 - GUI и HTTP - Иван
   
Это так , пока примерное распределение

# Описание базы данных и методов

### Ответсвенный за реализацию БД и Server.Dal : Артем Сергеевич
(Все же я попрбовал тут поработь с dapperom)
- Если есть вопросы как с ним работать, то можешь задавать вопросы. Если все же без него, то смотри сам как удобнее))
- SQL запросы лучше вынести в отдельный класс, например SqlQuery и сделать их как возвращаемые свойства,чтобы было чище и удобнее

## Таблицы базы данных

### Employees
Хранит основную информацию о сотрудниках:
- `Id` — уникальный идентификатор сотрудника.
- `FirstName` — имя сотрудника.
- `LastName` — фамилия сотрудника.
- `DepartmentId` — ссылка на отдел.
- `PositionId` — ссылка на должность.
- `HireDate` — дата приема на работу.
- `IsActive` — статус сотрудника (активный/уволенный).

### Departments
Хранит информацию об отделах:
- `Id` — уникальный идентификатор отдела.
- `Name` — название отдела.

### Positions
Хранит данные о должностях:
- `Id` — уникальный идентификатор должности.
- `Name` — название должности.

### Leaves
Учёт отпусков и больничных:
- `Id` — уникальный идентификатор записи.
- `EmployeeId` — ссылка на сотрудника.
- `LeaveType` — тип отсутствия (отпуск/больничный).
- `StartDate` — дата начала.
- `EndDate` — дата окончания.


⚠️ **Замечание:** Это примерные таблицы, если есть что добавить, обсуждаем. Также на основе этих таблиц уже будет делать основные классы в Models, а также классы DTO для GUI слоя 
---

## Методы

### EmployeeRepository
- `AddEmployeeAsync` — добавление нового сотрудника.
- `GetEmployeesAsync` — получение списка сотрудников.
- `GetEmployeeByIdAsync(int id)` — получение данных конкретного сотрудника.
- `GetEmployeesByDepartmentAsync(int departmentId)` — получение всех сотрудников отдела.
- `UpdateEmployeeAsync` — обновление данных сотрудника.
- `DeleteEmployeeAsync` — удаление сотрудника.
- 
### DepartmentRepository
- `AddDepartmentAsync(string name)` — добавление нового отдела.
- `GetDepartmentsAsync()` — получение списка всех отделов.
- `UpdateDepartmentAsync(int id, string newName)` — обновление информации об отделе.
- `DeleteDepartmentAsync(int id)` — удаление отдела (с проверкой на наличие сотрудников).

### PositionRepository
- `AddPositionAsync(string name)` — добавление новой должности.
- `GetPositionsAsync()` — получение списка должностей.
- `UpdatePositionAsync(int id, string newName)` — обновление должности.
- `DeletePositionAsync(int id)` — удаление должности (с проверкой на занятые должности).

### LeaveRepository
- `AddLeaveAsync` — добавление записи об отпуске или больничном.
- `GetLeavesByEmployeeAsync` — получение записей об отпусках или больничных для конкретного сотрудника.
- `GetLeavesByDateRangeAsync(DateTime start, DateTime end)` — получение всех отпусков за указанный период.
- `GetLeaveBalanceAsync(int employeeId)` — получение остатка отпускных дней сотрудника.
- `CancelLeaveAsync(int leaveId)` — отмена отпуска или больничного.

### EmployeeSearchService
- `SearchEmployeesAsync` — поиск сотрудников по имени или фамилии.

### ReportService
- `GenerateEmployeeReportAsync(int employeeId)` — отчет по конкретному сотруднику.
- `GenerateDepartmentReportAsync(int departmentId)` — отчет по сотрудникам отдела.
- `GenerateLeaveStatisticsAsync(DateTime start, DateTime end)` — статистика по отпускам и больничным за период.

⚠️ **Замечание:** Это примерный скелет. Если есть пожелания,замечания или поправки вносите коррективы

### На основе этих методов и бд можно реализовывать уже BL и всю остальную логику приложения




