FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["src/Template.Application/Template.Application.csproj", "Template.Application/"]
RUN dotnet restore "Template.Application/Template.Application.csproj"
COPY ./src .

WORKDIR "/src/Template.Application"
RUN dotnet build "Template.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Template.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Application.dll"]