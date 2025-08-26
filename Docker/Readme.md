# Sinus Synchronous Docker Setup
This is primarily aimed at developers who want to spin up their own local server for development purposes without having to spin up a VM, but also works for production ready servers, granted you have the knowledge to configure it securely.
Obligatory requires Docker to be installed on the machine.

There are two directories: `build` and `run`

## 1. Build Images
To build a single image, or multiple images, use either `windows.ps1` or `linux.sh`.
There is two ways to build the necessary docker images:
- `-local` (`-Local` for Windows) will run the image build against the current locally present sources
- `-git` (`-Git` for Windows) will run the image build against the latest git main commit

Alongside that, you will need to specify what images to build. You have a few options here:
- Either `-all` (`-All` for Windows) to build all images
- Or `-server`, `-authservice`, `-services`, `-staticfileserver` (`-Server`, `-AuthService`, `-Services`, `-StaticFilesServer` for Windows) for the respective images. You can bundle these in the same command.

Here are a few examples:

```bash
# Build all images using local sources
./linux.sh -local -all

# Build the server and auth service images using git sources
./linux.sh -git -server -authservice

# Build the services image using local sources
./linux.sh -local -services
```

```ps1
# Build all images using local sources
./windows.ps1 -Local -All

# Build the server and auth service images using git sources
./windows.ps1 -Git -Server -AuthService

# Build the services image using local sources
./windows.ps1 -Local -Services
```


## 2. Configure ports + token
Head to `run/compose` and make a copy of the `.env.example` file to `.env.local` and edit the environment variables inside of there with the appropriate values.
The Docker Compose file uses Cloudflare Tunnel for simplified access to the services.

In your Cloudflare Tunnel, you should configure the following under Public hostnames in this order:

|   | Public hostname          | Path  | Service                  |
|---|--------------------------|-------|--------------------------|
| 1 | sinus.<your_domain>      | auth  | http://sinus-auth:6500   |
| 2 | sinus.<your_domain>      | oauth | http://sinus-auth:6500   |
| 3 | sinus.<your_domain>      | *     | http://sinus-server:6000 |
| 4 | sinuscdn.<your_domain>   | *     | http://sinus-files:6200  |
| 5 | sinusstats.<your_domain> | *     | http://grafana:3000      |

Naturally, you can also do the proxying with another service or on your own.

## 3. Run Mare Server
Head to `run` and start the services using either `\.linux.sh` or `\.windows.ps1`.
There are two modes, each mutually exclusive:
- `-standalone` (`-Standalone` for Windows) to run the services as a single instance.
- `-sharded` (`-Sharded` for Windows) to run the services in a sharded configuration.

By supplying `-start` (`-Start` for Windows), the services will be started in the background. To stop them, you can use the `-stop` (`-Stop` for Windows) flag.
If you do not provide either `-start` or `-stop`, the services will run in the foreground.

Here are a few examples:

```bash
# Start the services in standalone mode
./linux.sh -standalone -start

# Start the services in sharded mode
./linux.sh -sharded -start

# Stop the services
./linux.sh -stop

# Start in the foreground
./linux.sh -standalone
```

```ps1
# Start the services in standalone mode
./windows.ps1 -Standalone -Start

# Start the services in sharded mode
./windows.ps1 -Sharded -Start

# Stop the services
./windows.ps1 -Stop

# Start in the foreground
./windows.ps1 -Standalone
```