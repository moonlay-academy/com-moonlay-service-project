FROM microsoft/aspnetcore:1.1
WORKDIR App
EXPOSE 80
COPY ./bin/publish/. . 
ENTRYPOINT ["dotnet","Com.Moonlay.Service.Project.WebApi.dll"]