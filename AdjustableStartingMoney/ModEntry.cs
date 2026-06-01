using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace VetoCV.AdjustableStartingMoney
{

    internal class ModEntry : Mod
    {
        private ModConfig Config = null!;
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        }

        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            if (Game1.stats.DaysPlayed == 1)
            {
                Game1.player.Money = Config.StartingMoney;
                Monitor.Log($"Applied starting money: {Config.StartingMoney}g", LogLevel.Info);
            }
        }

    }
}
