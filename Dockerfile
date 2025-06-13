# 使用官方 .NET 8 SDK 建構映像
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# 複製專案檔
COPY . ./

# 還原 NuGet 套件
RUN dotnet restore

# 建立發布版本
RUN dotnet publish -c Release -o out

# 執行階段映像（更小、更快）
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

# 啟動你的應用程式
ENTRYPOINT ["dotnet", "Server.dll"]