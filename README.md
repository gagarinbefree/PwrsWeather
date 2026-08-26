[![.NET Tests](https://github.com/gagarinbefree/PwrsWeather/actions/workflows/dotnet-tests.yml/badge.svg)](https://github.com/YOUR_USERNAME/PwrsWeather/actions/workflows/dotnet-tests.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-InteractiveServer-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![MediatR](https://img.shields.io/badge/MediatR-12.4.1-0078D4)](https://github.com/jbogard/MediatR)
[![AutoMapper](https://img.shields.io/badge/AutoMapper-13.0.1-0078D4)](https://automapper.org/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3.0-7952B3?logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![Bootstrap Icons](https://img.shields.io/badge/Bootstrap_Icons-1.11.3-7952B3?logo=bootstrap&logoColor=white)](https://icons.getbootstrap.com/)

# Weather App
Погодное веб-приложение на .NET 10 + Blazor с использованием Clean Architecture, CQRS/MediatR и Bootstrap.

## Установка и запуск
### 1. Клонировать репозиторий
### 1. В appsettings.json добавить ключ от WeatherAPI.com

## О проекте
Приложение отображает погодную информацию для города Москва:
- **Текущая погода** — температура, ощущается как, ветер, влажность, давление, УФ-индекс, видимость
- **Почасовой прогноз** — оставшиеся часы текущего дня и все часы следующего дня
- **Прогноз на 3 дня** — максимальная/минимальная/средняя температура, осадки, ветер, восход/закат

### Особенности
- ✅ Чистая архитектура (Clean Architecture)
- ✅ CQRS с MediatR
- ✅ AutoMapper для маппинга DTO → Domain
- ✅ Интерактивный Blazor (InteractiveServer)
- ✅ Bootstrap 5 + Bootstrap Icons
- ✅ Обработка ошибок с кнопкой Retry
- ✅ Адаптивный дизайн

## Технологии
| Технология | Версия |
|------------|--------|
| .NET | 10.0 |
| Blazor | InteractiveServer |
| MediatR | 12.4.1 |
| AutoMapper | 13.0.1 |
| Bootstrap | 5.3.0 |
| Bootstrap Icons | 1.11.3 |

## Требования
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) или другой редактор
- API ключ от [WeatherAPI.com](https://www.weatherapi.com/)
