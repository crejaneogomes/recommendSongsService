# Recommend Songs Service
- A microservice to register users and recommend music based on the temperature of the user's hometown.
- The project was built using the ASP.Net Core 3.1 framework. It also makes use of Docker and Docker Compose.
- To execute the project it is necessary to install Microsoft's SDK 3.1 and install Docker and Docker Compose.
- Has integration with the spotify and OpenWeatherMap API.

# Containers
- The project uses 2 containers that are orchestrated in the project's docker-compose file
  - Container 1: RecommendSongsService service image - [Bakend]
  - Container 2: PostgresSQL SGBD image - [SGBD]
 
# Running the Project
- Clone the repository on your machine
- Go to the folder "recommendSongsService.API"
- Run the command in any terminal: <b> docker-compose up </b>
- a Swagger with descriptions of the endpoints can be accessed through the URL http://localhost:5000/swagger/index.html or http://localhost:5001/swagger/index.html
- The application is only available in local mode at the urls: http: //localhost:5000/ and http://localhost:5001/
Some considerations:
- To execute the Music Recommendation request, you must first register a user, go through the login route to authenticate and receive a token. Finally, use this token to authorize the recommendation request;

Visual Studio Code was the IDE chosen for the development of the project, to open the project just go to file-> opne folder and select the project folder.

# Solution Overview
https://drive.google.com/file/d/1aHkuVpBfGyDpb3dSJoIQSR9FNBkK2g6y/view?usp=sharing)
