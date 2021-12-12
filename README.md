C# .Net Exam project

# Payman REST API and Xamarin app #

Course: 62413 Advanced object oriented programming with C# and .NET ##

Created by student: s215777

About Payman
==================
Most student workers have at some point in their student life been in the situation where the wage from a
part time job and shifting hours has been hard to predict. In short, how much money am I going to earn
this month? Unless you yourself keep track of it with extensive excel spreadsheets, or your place of work
happens to always have a very convenient time scheme available for you, this application might be of
use. An extended goal of this application would be to also calculate the different taxes and other
deductions that are involved, as well as tracking your tax card (frikort, hovedkort, bikort).

Instructions
==================
This project contains two Visual Studio solutions, one of them being for a backend REST API build with ASP.NET and the other a Xamarin crossplatform app
The requires solutions files can be found in the following directories:
- ASP.NET REST API: PayMan/PayManAPI/PayManWebAPI.sln
- Xamarin app: PayMan/PayManXamarin/PayManXamarin.sln

Backend API setup
==================
Technologies used in the backed API are as follows:

- ASP.NET Core
- Swagger OpenAPI documentation
- JWT token authentication
- MongoDB NoSQL database
- Docker

Xamarin client app setup
==================
Technologies used in the frontend client app are as follows:

- Xamarin.Forms 
- Xamarin.Essentials
- Newtonsoft.Json
- Refactored.MvvmHelpers
- Android Emulator