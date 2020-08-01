FROM mcr.microsoft.com/dotnet/core/sdk:3.1

RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 5000
EXPOSE 5001

CMD ["dotnet", "run"]