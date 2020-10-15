FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .


# Run the image as a non-root user
RUN useradd -m myuser
USER myuser



CMD ASPNETCORE_URLS=http://*:$PORT ConnectionStrings__IsSSL=true ConnectionStrings__CollectionName=$COLLECTION_NAME ConnectionStrings__DatabaseName=$DATABASE_NAME ConnectionStrings__ConnectionString=$CONNECTION_STRING dotnet urlShortener.dll

