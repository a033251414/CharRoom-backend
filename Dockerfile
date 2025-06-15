#── build 階段 ───────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 複製並還原
COPY Server/*.csproj ./  
RUN dotnet restore  

# 複製程式碼並 publish
COPY Server/. .   
RUN dotnet publish -c Release -o /app/publish

#── runtime 階段 ─────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Server.dll"]