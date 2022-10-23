FROM bitnami/dotnet-sdk:latest as build-image

WORKDIR /work
COPY ./ ./

RUN dotnet publish -c Release
RUN mv bin/Release/net6.0/linux-x64/publish/bootstrap bootstrap
RUN chmod +x bootstrap

FROM public.ecr.aws/lambda/provided:al2

COPY --from=build-image /work/bootstrap /var/runtime/

CMD ["dummyHandler"]
