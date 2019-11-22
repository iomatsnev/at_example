using Common.Instances;
using Common.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Common.Configs;

namespace Common.Tests
{
    /// <summary>
    /// Base Test
    /// </summary>
    public class BaseTest
    {
        [OneTimeSetUp]
        public void BaseSetUp()
        {
            Logger.InitLogger(TestContext.CurrentContext.Test.Name);

            // Kill all running applications and BOSM launcher
            foreach (InstanceConfig instanceConfig in ConfigManager.Configs.Values)
            {
                Instance.Kill(instanceConfig);
                Instance.ClearUserSettings(instanceConfig);
                Instance.CopyUserSettings(instanceConfig);
            }
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            InstanceManager.RemoveAllInstances();
        }

        [TearDown]
        public void TearDown()
        {
            if ((TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                && (InstanceManager.CurrentInstance?.CurrentSession != null))
            {
                ScreenshotHandler.TakeScreenshot(InstanceManager.CurrentInstance.CurrentSession);
            }
        }
    }
}
