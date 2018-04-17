# A Developer's Guide to Docker - Lee Brandt

`It works on my machine. We’ve all heard it. Most of us have said it. It’s been impossible to get around it… until now. Not only can Docker-izing your development environment solve that issue, but it can make it drop-dead simple to onboard new developers, keep a team working forward and allow everyone on the team use their desired tools! I will show you how to get Docker set up to use as the run environment for your projects, how to maintain the docker environment, and even how easy it will be to deploy the whole environment to production in a way that you are actually developing in an environment that isn’t just “like” production. It IS the production environment! You will learn the basics of Docker, how to use it to develop and how deploy your “development” environment as the production environment!`

Containers share the host's resources - don't have to install operating systems on separate virtual machines, don't have to decide how much RAM or other resources to set aside for them.

VM takes up resources - even if it's not currently being used. Container takes up as much resource at it currently needs - doesn't take more.

### Images and Containers

Images - think classes in OOP

Container - think object in OOP

### Commands:

List of images:
```cmd
docker image list
```

```cmd
docker pull nginx:1.5.8
```

```cmd
docker run -it nginx /bin/bash
```

-it - run in interactive mode, marry the commands from the container to my terminal

/bin/bash - the process running

```cmd
docker container list
```

-a switch to show all - whether running or not

```cmd
docker stop app
docker stop [id]
```

```cmd
docker rm [id]
```

getting rid of orphan containers:

```cmd
docker system prune
```

### Dockerfile

Named `Dockerfile` by convention

```txt
FROM node:8.4

COPY . /app

WORKDIR /app

RUN npm install

CMD ["npm", "start"]
```

RUN `shell style` - usually runs as root
CMD - guaranteed to run after container is up

```cmd
docker build .          // if named by convention
docker build MyFile     // if docker filed named something else
```

### Docker Compose

Let's you give instructions to Docker

```txt

```