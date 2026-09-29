# BankApp

Консольное приложение «Управление банковскими счетами» на C# и .NET 8.

Автор: Лыткин Максим Константинович.

## Запуск

Откройте `BankApp.sln` в Visual Studio 2022 с установленным .NET 8 SDK и запустите проект сочетанием Ctrl+F5. Из терминала:

```shell
dotnet build BankApp.sln
dotnet run --project BankApp/BankApp.csproj
```

## Этапы

| Этап | Содержание | Версия |
|---|---|---|
| 1 | Базовый класс и поля | `stage-1` |
| 2 | Инкапсуляция и валидация | `stage-2` |

Для открытия отдельного этапа: `git switch --detach stage-2`. Возврат к итоговой версии: `git switch main`.
