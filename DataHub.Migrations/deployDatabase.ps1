Param(
	[parameter(Position=0)]
	[String]
	$db_server = '(local)',

	[parameter(Position=1)]
	[String]
	$db_name = 'SDCOE_DataHub', 

	[parameter(Position=2)]
	[String]
	$drop_database = $false,

	[parameter(Position=3)]
	[String]
	$env = 'LOCAL'
)

$Error.Clear()
$ErrorActionPreference = "Stop"

$scripts_dir = "$PSScriptRoot\sql"
$roundhouse_output_dir = "$PSScriptRoot\output"
$db_connectionString = "Server=$db_server;Database=$db_name;MultipleActiveResultSets=true;Trusted_Connection=true;"

$null = dotnet tool restore

if($drop_database -eq $true) {
	Write-Host "Dropping and recreating database $db_name using RoundhousE"
	dotnet rh -c $db_connectionString --drop --silent -o $roundhouse_output_dir
} else { 
	Write-Host "Updating database $db_name using RoundhousE"
}

dotnet rh -c $db_connectionstring --commandtimeout=300 -f $scripts_dir --env $env --silent -o $roundhouse_output_dir --transaction
