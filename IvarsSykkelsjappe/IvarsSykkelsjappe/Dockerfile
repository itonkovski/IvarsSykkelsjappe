#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IvarsSykkelsjappe/IvarsSykkelsjappe.csproj", "IvarsSykkelsjappe/"]
RUN dotnet restore "IvarsSykkelsjappe/IvarsSykkelsjappe.csproj"
COPY . .
WORKDIR "/src/IvarsSykkelsjappe"
RUN dotnet build "IvarsSykkelsjappe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IvarsSykkelsjappe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IvarsSykkelsjappe.dll"]
