# ParkNGo Web Application
A web application that provides information, tools and services related to parking. The purpose of ParkNGo is to provide users with the best parking alternative so that they can plan their trip accordingly. ParkNGo will also allow users to rent out their parking spots to the public, like an Airbnb, where property owners can set their rate and schedule.

* Video Demo ([here](https://www.youtube.com/watch?v=42SRPGvkVDg))
* Detailed Documentation ([here](https://drive.google.com/file/d/1i3HaFjKwhXwE7Mzibd7Sq-sdD7mn6veu/view?usp=sharing))

![Park N Go Main Screen](https://github.com/thanhtrannn/ParkNGo/blob/master/parkngo-front.png?raw=true)
## Getting Started
### Database
**Microsoft SQL Server 2016:**
* **Installed Features:** 
    * Database Engine Services
    * Client Tool Connectivity
    * Integration Services

Replicate instance of database by running backup (.bak) file or by replicating model found in **ParkNGo/Model/** or **ParkNGo/Data/ParkNGoContext.cs**

### Environment and Configuration

* **Development:** Visual Studio IIS Express
* **Staging:** Contained deployment to IIS with Core .NET 2.1 Hosting Bundle (Capable of cross platform deployment)

Set environment variables such as connection string through appsettings.**{Development/Staging}**.json


## Testing
Using Selenium IDE, run the file ParkNGo Test.side. An automated web testing suite containing multiple unit tests to verify features and functions.

## Usage
After successful deployment and passed test suite, open web browser of your choice and browse to the web application. Create account through new user form found on landing page or create user through sql with role(Initial admin must be initiated through this method).

## Authors
* **[Thanh Tran](https://github.com/thanhtrannn)**