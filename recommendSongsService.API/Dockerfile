FROM microsoft/dotnet:3.1-sdk

RUN apt update && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /vsdbg

COPY . /app
WORKDIR /app

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 51290

RUN chmod +x ./entrypoint.sh
CMD ["dotnet", "run"]