# Transport For London - Coding Challenge

### How to build the code:-

**Prerequisites for Building the Code:-**

* .Net 5
* Visual Studio 2019 or 2022

Clone this repository or download the code as a Zip file incase there are any permissions issues with cloning, make sure you have support for running .Net 5.0 in Visual Studio, initially I started writting the appilication in .Net6 and C#10 but in order to provide support for visual studio 2019 users I changed to .Net5.
Make sure **Tfl.Client** is selected as the startup project then use Visual Studio or .Net CLI to restore Nuget Packages then build the code. Building the code should prodcue an exe called **RoadStatus.exe**

### How to run the output:-

Building the code should produce an exe called **RoadStatus.exe** at this path: *tfl-challenge\src\Tfl.Client\bin\Debug\net5.0*. Once the exe is located use Powershell to execute the exe with this command: **.\RoadStatus.exe {Id}** or directly use Visual Studio to run the code. Incase you chose to run the code with Visual Studio then the road ID argument can be passed from startup project's properties.

**Passing arguments:-**

Application is expecting comma separated road Id(s). In case you are passing a road Id with an empty space in it eg: **blackwall tunnel** then make sure to escape such Id in the following format: **blackwall%20tunnel**

Below are some examples of running the application with different arguments:-

**Valid Id:** .\RoadStatus.exe A2

**Output:-**
![alt text](https://github.com/FatehAhmad/static-assets/blob/main/screenshots/tfl/RoadStatus.exe%20A2.png)

**Valid Comma Separated Ids:** .\RoadStatus.exe A2,A1

**Output:-**
![alt text](https://github.com/FatehAhmad/static-assets/blob/main/screenshots/tfl/RoadStatus.exe%20A2%2CA1.png)

**Valid Whitespace Escaped Id:** .\RoadStatus.exe blackwall%20tunnel

**Output:-**
![alt text](https://github.com/FatehAhmad/static-assets/blob/main/screenshots/tfl/RoadStatus.exe%20blackwall%2520tunnel.png)

**Invalid Id containing whitespace:** .\RoadStaus.exe "a 2"

**Output:-**
![alt text](https://github.com/FatehAhmad/static-assets/blob/main/screenshots/tfl/RoadStaus.exe%20'a%202'.png)


### How to run the tests:-

Tests can be executed from Visual Studio test explorer or .Net CLI.


### App_Id, App_Key and other configurations:-

My approach was to make the software highly configurable and to achieve this I have added the following settins in **appsettings.json**:-

* **ApiBaseUrl**: Base Url for the Tfl endpoint
* **AppId**: Optional, if not present in the appsettings.json then a request is made without this value.
* **AppKey**: Optional, if not present in the appsettings.json then a request is made without this value.
* **Path**: Path for the road endpoints, ie: road

The point of making the BaseUrl and Path configurable is that Urls often so having them in appsettings.json would make it easier for us to update these values if required.

### Assumptions:-

I have build the software assuming that it is highly possible that I might have to extend the functionality of this application in the future and for this reason I have tried to follow **Clean Architecture** and I have added the following layers in the software:-

* **Client:** Top most layer with which the end users are interacting.
* **Application:** Contains the abstractions.
* **Infrastructure:** Implementation of the abstractions.
* **Domain:** Model entities.
* **Common:** Extensions and utilities methods which can be used accross the project.

### Design and Technical Details:-

The idea here is to keep the software simple, yet easily extendable, make the software loosely coupled and rely on abstractions instead relying details and make the software easily testable. The point of keeping abstraction and implementations apart is to be able to replace implementation details easily without having the consumer notified that implementation details are changed.

Looking forward to any suggestions and feedback! :)















