#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

#DOCX BUILDER
#####################

RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig

RUN apt-get update; apt-get install -y fontconfig fonts-liberation
RUN fc-cache -f -v

RUN apt-get install -y --no-install-recommends libgdiplus libc6-dev 

#####################
#END DOCX BUILDER

#PUPPETEER RECIPE
#####################
RUN apt-get update && apt-get -f install && apt-get -y install wget gnupg2 apt-utils
RUN wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add -
RUN echo 'deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main' >> /etc/apt/sources.list
RUN apt-get update \
&& apt-get install -y google-chrome-stable --no-install-recommends --allow-downgrades fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf 


######################
#END PUPPETEER RECIPE
ENV PUPPETEER_EXECUTABLE_PATH "/usr/bin/google-chrome-stable"




FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM base   
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "CVBuilder.Web.dll"]