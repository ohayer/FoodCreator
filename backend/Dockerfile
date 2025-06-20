FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app/backend

COPY . .

RUN dotnet restore 
RUN dotnet publish -o out 


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app/backend
COPY --from=build /app/backend/out .

EXPOSE 3000 
ENV ASPNETCORE_URLS=http://+:3000
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet","FoodCreator.dll"]


