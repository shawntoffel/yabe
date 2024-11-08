FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./**/*.csproj ./
RUN for file in *.csproj; do mkdir ${file%.*} && mv $file ${file%.*}; done
ARG CSPROJ=Yabe.Console
RUN dotnet restore $CSPROJ
COPY . .
RUN dotnet publish $CSPROJ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.20
RUN apk add --no-cache tzdata icu-libs
WORKDIR /app
COPY --from=build /app/out .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8
CMD ["dotnet", "Yabe.Console.dll"]
