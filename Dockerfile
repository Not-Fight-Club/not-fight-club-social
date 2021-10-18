FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SocialApi/SocialApi.csproj", "SocialApi/"]
COPY ["SocialApi_Test/SocialApi_Test.csproj", "SocialApi_Test/"]
RUN dotnet restore "SocialApi\SocialApi.csproj"
RUN dotnet restore "SocialApi_Test\SocialApi_Test.csproj"
COPY . .
WORKDIR "/src/SocialApi"
RUN dotnet build "SocialApi.csproj" -c Release -o /app/build
RUN dotnet build "/src/SocialApi_Test/SocialApi_Test.csproj" -c Release -o /app/build

RUN dotnet test "/src/SocialApi_Test/SocialApi_Test.csproj" --logger "trx;LogFileName=SocialApi_Test.trx"

FROM build AS publish
RUN dotnet publish "SocialApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialApi.dll"]
