# Gemini Telegram Bot

![Telegram](https://img.shields.io/badge/Telegram-Bot-blue?logo=telegram)
![.NET](https://img.shields.io/badge/.NET-8.0-blue)

**Gemini Telegram Bot** — это простой Telegram-бот на C# с архитектурой Clean Architecture, который использует модель Gemini (Google AI) для генерации ответов на сообщения пользователей. Проект реализован как pet-проект для демонстрации чистой архитектуры, DI, конфигурации через `appsettings.json` и работы с внешними API.

---

## Содержание

- [Функциональность](#Функциональность)
- [Структура проекта](#Структура-проекта)
- [Технологии](#Технологии)
- [Установка и запуск](#Установка-и-запуск)
- [Конфигурация](#Конфигурация)
- [Архитектура](#Архитектура)
- [Примеры сообщений](#Примеры)

---

## Функциональность

- Обработка команд `/start` и текстовых сообщений от пользователей.
- Генерация ответов через Google Gemini API.
- Безопасная отправка и редактирование сообщений в Telegram.
- Логирование действий бота и ошибок.
- Чистая архитектура с разделением слоев:
  - Presentation
  - Application
  - Domain
  - Infrastructure
- Конфигурируемый API ключ и Telegram токен через `appsettings.json`.

---

## Структура проекта

### Program.cs
Точка входа приложения, настройка DI, конфигурации и запуск бота.

### Application
Слой бизнес-логики и команд:
- **Commands** – команды для обработки сообщений (`StartMessageCommand`, `UserMessageCommand`)
- **Handlers** – обработчики команд и диспетчер сообщений (`ICommandHandler`, `MessageDispatcher`, `StartCommandHandler`, `UserMessageCommandHandler`)
- **Services** – абстракции для логирования и других сервисов (`ILogService`)

### Domain
Основные доменные сущности и контракты:
- **ValueObjects** – объекты сообщений (`MessageRequest`, `MessageResponse`)
- **ClientInterfaces** – интерфейсы для внешних клиентов (`IGeminiApiClient`, `IGeminiReponseParser`, `IResponseGenerator`, `IGeminiEnvelopeExtractor`)
- **Formatters** – интерфейсы форматирования сообщений (`IMessageFormatter`)

### Infrastructure
Реализация внешних зависимостей:
- **Gemini** – интеграция с API Gemini:
  - API клиент (`GeminiApiClient`)  
  - Парсер ответов (`GeminiResponseParser`)  
  - Генератор запросов (`GeminiRequestJsonFactory`, DTOs)
- **Loggers** – реализация логирования (`ConsoleLogService`)
- **Options** – конфигурационные DTO (`GeminiApiOptions`, `TelegramBotOptions`)
- **TelegramBotInfrastructure** – обертки для Telegram Bot API:
  - Клиент адаптер (`TelegramBotClientAdapter`)
  - Форматирование сообщений (`TelegramMarkdownMessageFormatter`)

### Presentation
Слой интерфейса приложения:
- **Telegram** – обработка обновлений Telegram (`TelegramUpdateHandler`)

---

## Технологии

- C# 11 / .NET 8
- Telegram.Bot API
- Google Gemini AI
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- Clean Architecture

---

## Установка и запуск

1. Клонируйте репозиторий:

```bash
git clone https://github.com/yourusername/GeminiTelegramBot.git
cd GeminiTelegramBot
```

2. Установите зависимости (для консольного приложения с .NET 8):
```bash
dotnet restore
dotnet build
```

3. Настройте appsettings.json:
```json
{
  "TelegramBot": {
    "Token": "YOUR_TELEGRAM_BOT_TOKEN"
  },
  "GeminiApi": {
    "ApiKey": "YOUR_GOOGLE_GEMINI_API_KEY"
  }
}
```

5. Запустите приложение
```bash
dotnet run
```

Бот начнет слушать входящие сообщение в Telegram

---

## Конфигурация
- **TelegramBot:Token** — токен вашего бота Telegram.
- **GeminiApi:ApiKey** — ключ доступа к Google Gemini API.

Все параметры подгружаются через IOptions<T> и appsettings.json.

---

## Архитектура
- **Presentation**: работа с Telegram API (TelegramUpdateHandler).
- **Application**: обработка команд, бизнес-логика, диспетчер команд.
- **Domain**: сущности и контракты для внешних API.
- **Infrastructure**: реализации логирования, API клиентов и форматеров.

Поток сообщений:
```nginx
TelegramUpdateHandler → MessageDispatcher → CommandHandler → ResponseGenerator → ApiClient → Parser → Adapter → Telegram
```

## Примеры
```
/start → "Можете отправить свой запрос"
```
```
Любое сообщение пользователя → Ответ от модели Gemini.
```
### Как это выглядит в Telegram:
<img width="642" height="702" alt="image" src="https://github.com/user-attachments/assets/debdc177-47c7-4f3b-9ee9-1a94e5e2c550" />

### Как это выглядит в консоли:
<img width="1245" height="512" alt="image" src="https://github.com/user-attachments/assets/4dbd50d3-6320-4892-b0b5-938af98d7337" />
