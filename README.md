# Demo project to show simple Reqnroll scenarios using Playwright, RestSharp and C#
  
Subjects which are covered in this demo project:
1. [x] Basic concept of feature and scenario using Reqnroll
2. [x] Concept of using same feature, scenario and step definitions for checking API and UI
3. [x] Concept of including features and scenarios to be executed manually and so excluded from testruns. Using this concept results in having the complete testset documented at one place
4. [x] Checking API, using RestSharp
5. [x] Checking UI of webapp, using PageObjects and Playwright
6. [x] Running scenarios in parallel
7. [x] Running scenarios from your IDE
8. [x] Running scenarios from commandline
9. [x] Filtering scenarios, by tag
10. [x] Using a test configuration file with possibility to override parameters from commandline
11. [x] Screenshot is created in case of failure of scenario in UI testrun 


https://automationexercise.com is used as test website for this
#
Features and scenarios can be started from your IDE and from commandline

Running from Visual Studio:

- Set "Auto Detect runsettings Files" to checked (by using Test Explorer - Configure Run Settings)
- Run test(s) from Test Explorer


Running from commandline:

- Parameters needed to start feature/scenario:
	- --filter TestCategory=\<ui or api> => needed to execute ui scenarios OR api scenarios
	- --settings \<name .runsettings file> => needed to get/set configuration for testrun

	Optional:
	- -- TestRunParameters.Parameter(name=\\"<name>\\", value=\\"<value>\\") => needed if parameter from .runsettings file needs to be overridden
	
	E.g. dotnet test --filter TestCategory=ui --settings .runsettings -- TestRunParameters.Parameter(name=\\"baseUrl\\", value=\\"http://automationexercise.com\\")

- See RunTestsAll.bat how all tests are started, splitted in a testrun for api and a testrun for ui
