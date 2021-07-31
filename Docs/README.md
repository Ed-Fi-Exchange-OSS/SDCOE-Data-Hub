# SDCOE Data Hub Prototype
This repo contains the DataHub API back end, React web app, and RoundHousE database deployment scripts. 

## Getting Started
For development, you will need:
* A local Microsoft SQL Server instance 
* Visual Studio 2019 for the API backend 
* Visual Studio Code for the React client web app

In a Powershell admin prompt, run the provided `build.ps1` script. This will: 
* deploy the SDCOE_DataHub database on your local SQL instance
* build the .net core API backend
* run `yarn build` on the web app

Note: For email capabilities, the application has MailJet integration as default SMTP server. 

## Deployment Customizations 

**Application Name, Logos and Coloration**

- Find the application name and logo image file references in `/DataHub.Web/DataHub.Client.Web/public/manifest.json ` and look for `[YOUR ORGANIZATION]` to replace in the JSON.  There are references to locations in the folder for `favicon.ico`, a 192x192 `logo192.png`, and a 512x512 `logo512.png`. Notes:
  - The main logo referenced in the application code is the file “Logo.png” within the project.
  - Two other logo files within the project are “header-logo.svg” and footer-logo.svg”.
  - The favicon is the file “favicon.ico” within the project.
- Color settings for the UI are contained in `/DataHub.Web/DataHub.Client.Web/index.css`
- To update the interface to include your organization name and contact info, these can be updated within `DataHub.Web/DataHub.Client.Web/src/components/Footer.tsx` where you can replace [YOUR ORGANIZATION], [ADDRESS], [CITY-STATE-ZIP] and [PHONE].
- There may be locations in code and content where `SDCOE` is included for display purposes. These may be replaced (when not part of application code) with another collaborative or organization’s name or abbreviation.

**Application URL**

- Update application URL within  `/DataHub.Web/DataHub.Client.Web/src/components/Navigation.tsx`  so that the application refers to the domain and base URL for your installation.  Look for [YOUR SITE URL]. 

**Sample database contents**:  

- The file `/DataHub.Migrations/sql/runFirstAfterUp/0011_CreateDemoData.env.LOCAL.sql` populates many tables with sample data. It is a good place to start if there is the desire to customize an installation.  There are a number of table INSERT statements here that can be altered/updated to suit your needs.
  - Look for `[ACCOUNT]`, `[YOUR ORGANIZATION WEB SITE]` and `[COLLABORATIVE DOMAIN]` for easy-to-find replacements. 
  - Note that the names, email addresses and phone numbers have been randomly generated.

**User emails, authentication**

- Azure AD settings: see `OpenId` block in `/DataHub.Web/DataHub.Api/appsettings.json` 
- SMTP user settings: see `SmtpSettings` block in `/DataHub.Web/DataHub.Api/appsettings.json` 

**SMTP Server**

- If you would like to change the default SMTP service (MailJet), this can be updated in:  `/DataHub.Web/DataHub.Api/Services/EmailService.cs`

## Creating/Updating the database 

In Powershell, navigate to `DataHub.Migrations` and run `deployDatabase.ps1`. You can specify the following optional parameters: 

| Parameter       | Default Value   | Purpose                                  |
|-----------------|-----------------|------------------------------------------|
| `db_server`     | `(local)`       | The SQL server/instance to connect to    |
| `db_name`       | `SDCOE_DataHub` | Name of the database to create/update    |
| `drop_database` | `$false`        | Whether to drop the existing database and create fresh. Updates an existing database by default. Specify `$true` to do a fresh deployment |
| `$env`            | `LOCAL`         | Environment string passed to RoundhousE to run environment-specific scripts |

## Running the API backend
Open the Visual Studio solution file, `DataHub.Web\DataHubApi.sln`. Then in Solution Explorer, right-click the `DataHub.Api` project and click "Set as Startup Project." 

Now press <kbd>F5</kbd> and a browser will launch. Once the page loads, you should see an `INFO` entry in the debug log indicating the GET request succeeded.

## Running the React client web app 
In Powershell, navigate to `DataHub.Web\DataHub.Client.Web` and type `code .` to launch the folder workspace in Visual Studio Code. From here, open the Npm Scripts panel in Explorer and run `build` to run the build task, then `start`. 

Now you can press <kbd>F5</kbd> to launch a browser and load the client app, pointed at your local backend API by default. Any debug breakpoints you set in the React code will now be hit in VSCode.

## Mocking API requests in client
The `.env` file in the client app root includes an environment variable named REACT_APP_USE_MOCK_SERVER which when set to "true" enables a mock server defined in `DataHub.Web\DataHub.Client.Web\src\serverMock.ts`. The API requests defined in `DataHub.Web\DataHub.Client.Web\src\api\api.ts` can then be stubbed using "/mock/api" instead of "/api" as the start of the path for the request. Examples are commented in both files.

## About React
This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).
