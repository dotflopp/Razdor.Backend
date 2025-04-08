## How to test with docker desktop
1) Setup docker
2) Clone repository
3) Go to project directory
4) Build docker image 
```shell
    docker build -t image-name .
```
5) Run container
```
    docker run --rm -p 5154:8080 image-name
```
6) Build and use [frontend](https://github.com/dotflopp/razdor-frontend) or view the available routes on http://**/swagger

If you have any issues with live streaming, please [write it](https://github.com/dotflopp/Razdor.Backend/issues)