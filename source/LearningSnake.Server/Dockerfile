#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LearningSnake.Server/LearningSnake.Server.csproj", "LearningSnake.Server/"]
COPY ["LearningSnake.NeuralNetwork/LearningSnake.NeuralNetwork.csproj", "LearningSnake.NeuralNetwork/"]
COPY ["LearningSnake.LearningSnakeManager/LearningSnake.LearningSnakeManager.csproj", "LearningSnake.LearningSnakeManager/"]
COPY ["LearningSnake.GeneticAlgorithm/LearningSnake.GeneticAlgorithm.csproj", "LearningSnake.GeneticAlgorithm/"]
COPY ["LearningSnake.GameOfSnake/LearningSnake.GameOfSnake.csproj", "LearningSnake.GameOfSnake/"]
COPY ["LearningSnake.DTO/LearningSnake.DTO.csproj", "LearningSnake.DTO/"]
COPY ["LearningSnake.CurrentConfiguration/LearningSnake.CurrentConfiguration.csproj", "LearningSnake.CurrentConfiguration/"]
COPY ["LearningSnake.Client/LearningSnake.Client.csproj", "LearningSnake.Client/"]

RUN dotnet restore "LearningSnake.Server/LearningSnake.Server.csproj"
COPY . .
WORKDIR "/src/LearningSnake.Server"
RUN dotnet build "LearningSnake.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LearningSnake.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LearningSnake.Server.dll"]