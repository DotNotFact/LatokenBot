# Установка и запуск бота

Этот документ содержит инструкции по установке и запуску Telegram бота.

## Предварительные требования

Для работы бота необходимы следующие компоненты:

- [.NET SDK](https://dotnet.microsoft.com/download) последней версии.
- Установленный [Git](https://git-scm.com/downloads) для клонирования репозитория.
- Действующий API ключ от OpenAI для ChatGPT.
- Действующий API ключ для Telegram Bot API.

## Клонирование репозитория

Сначала склонируйте репозиторий на вашу локальную машину используя Git:

```bash
git clone https://github.com/DotNotFact/LatokenBot.git
cd LatokenBot
```

## Настройка переменных сред

Перед запуском бота необходимо настроить API ключи. Откройте файл launchSettings.json в папке Properties и вставьте ваши API ключи в соответствующие поля:

```json
{
  "profiles": {
    "LatokenBot": {
      "commandName": "Project",
      "environmentVariables": {
        "chat_gpt_api_key": "<ВАШ_CHATGPT_API_KEY>",
        "telegram_api_key": "<ВАШ_TELEGRAM_API_KEY>"
      }
    }
  }
}
```

Этот файл `INSTALL.md` предоставляет полную информацию о том, как настроить и запустить бота, начиная с установки необходимых инструментов, заканчивая конфигурацией и запуском проекта.
