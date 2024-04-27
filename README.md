# Latoken bot for Telegram Group

Этот репозиторий содержит код для Telegram бота, написанного на языке C#. Бот использует технологии искусственного интеллекта для ответа на вопросы пользователей.

## Материалы для "тренировки" бота

Бот обучен на следующих файлах:
- `rules.txt`: Промт для телеграм бота.
- `about.txt`: Общая информация о Латокне.
- `hackaton.txt`: Данные о Хакатоне.
- `test.txt`: Тестовые вопросы.
- `hard.txt`: Сложное задание, информация с сайта.

## Задания

### 1) Для начинающих:
- **Задача**: Отобразите бота на экране и предоставьте ссылку на него жюри.
- **Описание**: Бот способен отвечать на вопросы, основанные на упомянутых выше материалах. Члены жюри могут напрямую задавать вопросы боту.

### 2) Для опытных:
- **Задача**: Помимо задач для начинающих, покажите на экране сделанную вами группу и пришлите на нее ссылку жюри.
- **Описание**: Жюри будет задавать вопросы по теме хакатона в эту группу, и бот должен отвечать на них непосредственно в группе.

### 3) Для продвинутых:
- **Задача**: Выполните все предыдущие задачи и добавьте функционал, при котором бот, ответив на вопрос, задает свои вопросы, тестируя кандидата на основе материалов хакатона.
- **Описание**: Это проверка не только на способность бота ответить на вопросы, но и на его способность вести интерактивное общение, задавая вопросы для тестирования знаний кандидата.

## Библиотеки

- [Dependency Injection](https://github.com/dotnet/docs/blob/main/docs/core/extensions/dependency-injection.md)
- [MongoDB Driver](https://github.com/mongodb/mongo-csharp-driver)
- [Semantic Kernel](https://github.com/microsoft/semantic-kernel)
- [Telegram Bot](https://github.com/telegrambots/Telegram.Bot)
- [Kernel Memory](https://github.com/microsoft/kernel-memory)

Для установки и запуска бота следуйте инструкциям в файле `INSTALL.md`.
