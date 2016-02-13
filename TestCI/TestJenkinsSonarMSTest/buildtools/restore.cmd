@echo off

set solution=%~dp0..\TestJenkinsSonarMSTest.sln

%~dp0.\nuget.exe restore %solution% -Source NEXUS -FallbackSource nuget.org -ConfigFile %~dp0.\nuget.config
