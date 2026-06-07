# Exemplo .NET 10 + Datadog com Docker

## Configuração

1. Copie `.env.example` para `.env` e adicione sua chave de API do Datadog:
   ```bash
   cp .env.example .env
   # Edite .env com seu DD_API_KEY
   ```

2. Compile e execute:
   ```bash
   docker-compose up --build
   ```

3. Teste os endpoints:
   - `http://localhost:8080/` - Health check
   - `http://localhost:8080/trace` - Gera um trace personalizado

## Funcionalidades do Datadog Habilitadas

- **APM Tracing**: Instrumentação automática do ASP.NET Core + HttpClient
- **Traces Personalizados**: Criação manual de spans no endpoint `/trace`
- **Injeção de Logs**: IDs de correlação injetados nos logs
- **DogStatsD**: Coleta de métricas na porta 8125
- **Integração Serilog**: Logging estruturado para o Datadog

## Configuração

Variáveis de ambiente:
- `DD_API_KEY` - Sua chave de API do Datadog (obrigatório)
- `DD_SERVICE` - Nome do serviço (padrão: example-dotnet-datadog-app)
- `DD_ENV` - Ambiente (padrão: development)
- `DD_VERSION` - Versão do app (padrão: 1.0.0)

## Testes Realizados

![datadog](Imagens/dataDog.png)