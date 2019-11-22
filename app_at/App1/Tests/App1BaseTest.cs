using Common.Tests;
using Common.Utils;
using App1.Pages;
using App1.Services;
using NUnit.Framework;

namespace App1.Tests
{
    public class App1BaseTest : BaseTest
    {
        [OneTimeSetUp]
        public void PresetSystem()
        {
            SqlScriptPresetSystem.UpdateGlobalSetting();
            Logger.Info($"All Clobal Settings applied");
        }
    }
}
