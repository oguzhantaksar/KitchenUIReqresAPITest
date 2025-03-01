## Prerequisites

- .NET 8.0 SDK (or an appropriate version)

## Setup

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/oguzhantaksar/KitchenUIReqresAPITest.git
   cd KitchenUIReqresAPITest

2. **Restore Packages:**
    
    ```bash
    dotnet restore 

## Running the tests

    Run all tests with logs on the terminal and logs saved to testlog.txt:
    dotnet test --logger "console;verbosity=detailed" | tee testlog.txt

    Run all tests:
    dotnet test 

    Run all API tests:
    dotnet test --filter "FullyQualifiedName~EasternPeakAutomation.API.Tests"
    
    Run all UI tests:
    dotnet test --filter "FullyQualifiedName~EasternPeakAutomation.UI.Tests"
