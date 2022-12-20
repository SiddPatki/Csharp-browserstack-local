# Azure Plugin + csharp-selenium-browserstack

## Prerequisite
<b>Dotnet CLI</b> must be installed on your system. Check the installation exists before running script.

```
dotnet --version
```
## Steps to run test sessions
### Clone and build the project
```
git clone https://github.com/browserstack/csharp-selenium-browserstack.git
cd csharp-selenium-browserstack
dotnet build
```
### Configure credentials in each test files (SingleTest.cs, LocalTest.cs, Paralleltest.cs)
```c#
String BROWSERSTACK_USERNAME = "BROWSERSTACK_USERNAME";
String BROWSERSTACK_ACCESS_KEY = "BROWSERSTACK_ACCESS_KEY";
```
### Run Single Test
i. Navigate to Single.cs </br>
ii. Configure the capabilites

```csharp
Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
browserstackOptions.Add("osVersion", "14");
browserstackOptions.Add("deviceName", "iPhone 12");
browserstackOptions.Add("realMobile", "true");
browserstackOptions.Add("local", "false");
```
iii. Run your test <br/>
```
dotnet run Program.cs single
```
### Run Local Test
i. Navigate to Local.cs </br>
ii. Configure the capabilites

```csharp
Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
browserstackOptions.Add("osVersion", "14");
browserstackOptions.Add("deviceName", "iPhone 12");
browserstackOptions.Add("realMobile", "true");
browserstackOptions.Add("local", "true");
```
iii. Run your test <br/>
```
dotnet run Program.cs local
```
### Run Parallel Test
i. Navigate to Parallel.cs </br>
ii. Configure the capabilites

```csharp
Thread device1 = new Thread(obj => sampleTestCase("Safari", "latest", null, "14", "iPhone 12 Pro Max", "true", "iPhone 12 Pro Max - safari latest", "Parallel-build-csharp"));
Thread device2 = new Thread(obj => sampleTestCase("Chrome", "latest", null, null, "Samsung Galaxy S20", "true", "Samsung Galaxy S20 - Chrome latest", "Parallel-build-csharp"));
Thread device3 = new Thread(obj => sampleTestCase("Firefox", "latest", "OSX", "Monterey", null, null, "macOS Monterey - Firefox latest", "Parallel-build-csharp"));
Thread device4 = new Thread(obj => sampleTestCase("Safari", "latest", "OSX", "Big Sur", null, null, "macOS Big Sur - Safari latest", "Parallel-build-csharp"));
Thread device5 = new Thread(obj => sampleTestCase("Edge", "latest", "Windows", "10", null, null, "Windows - Edge latest", "Parallel-build-csharp"));
```
iii. Run your test <br/>
```
dotnet run Program.cs parallel
```
r
