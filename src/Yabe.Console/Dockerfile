FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ./**/*.csproj ./
RUN for file in *.csproj; do mkdir ${file%.*} && mv $file ${file%.*}; done
ARG CSPROJ=Yabe.Console
RUN dotnet restore $CSPROJ
COPY . .
RUN dotnet publish $CSPROJ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.13
RUN apk add --no-cache tzdata
WORKDIR /app
COPY --from=build /app/out .
CMD ["dotnet", "Yabe.Console.dll"]
