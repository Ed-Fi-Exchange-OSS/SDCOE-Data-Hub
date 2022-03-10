
$Error.Clear()
$ErrorActionPreference = "Stop"

try { 
	cd DataHub.Migrations
	. .\deployDatabase.ps1 -drop_database $true

	cd ..\DataHub.Web
	dotnet restore DataHubApi.sln
	dotnet build DataHubApi.sln

	cd DataHub.Client.Web
	yarn install 
	yarn build 

	Write-Output 'Local build completed'
}
catch { 
	Write-Output 'Local build was not successful; check logs for errors'
}
finally { 
	cd $PSScriptRoot
}
