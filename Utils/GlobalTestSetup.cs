using EasternPeakAutomation.Utils;

namespace EasternPeakAutomation;

[SetUpFixture]
public class GlobalTestSetup
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        LogHelper.InitializeLogger();
    }

    [OneTimeTearDown] 
    public void GlobalTearDown()
    {
        LogHelper.ShutdownLogger();
    }
}