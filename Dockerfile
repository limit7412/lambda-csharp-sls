# =====================================================================
# Docker(ECR イメージ)版の Dockerfile【旧構成・参考用にコメントアウト】
# ---------------------------------------------------------------------
# zip(provided.al2)版へ移行したため未使用。
# zip 版では serverless.yml の scripts フックが bitnami/dotnet-sdk イメージで
# `dotnet publish -c Release` を実行して bootstrap を生成するため、
# この Dockerfile は不要になった。
# ---------------------------------------------------------------------
# FROM bitnami/dotnet-sdk:latest as build-image
#
# WORKDIR /work
# COPY ./ ./
#
# RUN dotnet publish -c Release
# RUN mv bin/Release/net6.0/linux-x64/publish/bootstrap bootstrap
# RUN chmod +x bootstrap
#
# FROM public.ecr.aws/lambda/provided:latest
#
# COPY --from=build-image /work/bootstrap /var/runtime/
#
# CMD ["dummyHandler"]
# =====================================================================
