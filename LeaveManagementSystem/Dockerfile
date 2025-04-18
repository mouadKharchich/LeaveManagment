FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LeaveManagementSystem/LeaveManagementSystem.csproj", "LeaveManagementSystem/"]
COPY ["LeaveManagementSystem.Persistence/LeaveManagementSystem.Persistence.csproj", "LeaveManagementSystem.Persistence/"]
COPY ["LeaveManagementSystem.Domain/LeaveManagementSystem.Domain.csproj", "LeaveManagementSystem.Domain/"]
COPY ["LeaveManagementSystem.Application/LeaveManagementSystem.Application.csproj", "LeaveManagementSystem.Application/"]
COPY ["LeaveManagementSystem.Infrastructure/LeaveManagementSystem.Infrastructure.csproj", "LeaveManagementSystem.Infrastructure/"]
RUN dotnet restore "LeaveManagementSystem/LeaveManagementSystem.csproj"
COPY . .
WORKDIR "/src/LeaveManagementSystem"
RUN dotnet build "LeaveManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LeaveManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeaveManagementSystem.dll"]
