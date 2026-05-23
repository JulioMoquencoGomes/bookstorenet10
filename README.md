# BookStore - net10
\
**Requisitos mínimos:**
\
\
Docker Desktop: https://docs.docker.com/desktop/
\
\
**Comandos para buildar e executar o projeto**
\
\
*Backend:*
\
\
cd src
\
docker compose build
\
docker compose up
\
\
**Aplicação backend sendo executada, url sendo utilizada para teste:** http://localhost:8080/book
\
\
![Books](Images/backend.png)
\
\
*Frontend utilizando React:*
\
\
cd front
\
docker build -t frontendapp .
\
docker run -p 3001:3000 frontendapp
\
\
**Aplicação frontend sendo executada, url sendo utilizada para teste:** http://localhost:3001/
\
\
![Books](Images/front2.png)
