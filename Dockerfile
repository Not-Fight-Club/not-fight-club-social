FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SocialApi/SocialApi.csproj", "SocialApi/"]
RUN dotnet restore "SocialApi\SocialApi.csproj"
COPY . .
WORKDIR "/src/SocialApi"
RUN dotnet build "SocialApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialApi.dll"]
