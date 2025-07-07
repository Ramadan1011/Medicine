FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Medicine/Medicine.csproj Medicine/

RUN dotnet restore Medicine/Medicine.csproj

COPY . .

WORKDIR /src/Medicine
RUN dotnet publish Medicine.csproj -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Medicine.dll"]
