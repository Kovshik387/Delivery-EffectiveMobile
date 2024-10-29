# Служба доставки

Разработанное решение позволяет филтровать данные о доставках и получать их в удобном формате

## Запуск

### Docker

У вас должнен быть установлен [`docker compoese`](https://www.docker.com/products/docker-desktop/) версии более `1.27.0`

1. Клонирование репозитория `git clone https://github.com/Kovshik387/Delivery-EffectiveMobile`
2. Запуск docker контейнеров `docker compose up -d`
3. Перейдите по `http://localhost:8040/swagger/index.html` в вашем браузере

### Запуск не в контейнере

У вас должен быть установлен и запущен [`postgres`](https://www.postgresql.org/)

1. Клонирование репозитория `git clone https://github.com/Kovshik387/Delivery-EffectiveMobile`
2. Редактирование конфигурационного [файла](Systems/Delivery.Systems.DeliveryAPI/appsettings.json). В секции `DbSettings` в поле `ConnectionString` необходимо указать url базы данных
3. Скомпилировать приложение

## [Конфигурация](Systems/Delivery.Systems.DeliveryAPI/appsettings.json)
- В секции `Log` настройка запись логов в файл и переодичность создания нового файла. Включение/выключение вывод в файл/консоль 
- В секции `DbSettings` настройка подключения к базе данных и включение/выключение добавления тестовых данных

### Схема
- Путь к схеме `http://localhost:8040/swagger/v1/swagger.json`

### Связь
+ Telegram: [@yrulewet](https://t.me/yrulewet)
