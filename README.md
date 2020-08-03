# Recommend Songs Service
A microservice to register a user and recommend songs based on hometown.
O projeto foi construído utilizando-se o framework  ASP.Net Core 3.1. E também faz uso de Docker e Docker Compose. 
Para executar o projeto é necessário instalar o SDK 3.1 da Microsoft e instalar o Docker e Docker Compose.
Possui integração com a API do spotify e da OpenWeatherMap

# Containers
- O projeto utiliza 2 containers que estão orquestrado no arquivo de docker-compose do projeto
 - Container 1: Imagem do serviço RecommendSongsService - [Bakend]
 - Container 2: Imagem do SGBD PostgresSQL - [SGBD]
 
# Rodando o Projeto
- Clonar na sua máquina o repositório
- Ir para a pasta "recommendSongsService.API"
- Rodar o comando em algum terminal: <b>docker-compose up</b>
- um Swagger com as descrições dos endpoints pode ser acessado através da URL http://localhost:5000/swagger/index.html ou http://localhost:5001/swagger/index.html
- A aplicação está disponível apenas em modo local nas urls: http://localhost:5000/ e http://localhost:5001/

Algumas considerações:
- Para executar a request de Recomendação de músicas é necessário primeiramente cadastrar um usuário, passar pela rota de login para autenticar e receber um token. Ao fim, utilizar esse token para autorizar a request de recomendação;

O Visual Studio Code foi a IDE escolhida para o desenvolvimento do projeto, para abrir o projeto basta ir em file->opne folder e selecionar a pasta do projeto. 

# Visão Geral da Solução
![alt text](https://drive.google.com/file/d/1aHkuVpBfGyDpb3dSJoIQSR9FNBkK2g6y/view?usp=sharing)
