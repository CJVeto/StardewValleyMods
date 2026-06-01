using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Integrations.GenericModConfigMenu;
using StardewValley;

namespace VetoCV.AdjustableStartingMoney
{

    internal class ModEntry : Mod
    {
        private ModConfig Config = null!;
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;

            Monitor.Log($"Loaded config: StartingMoney = {Config.StartingMoney}g", LogLevel.Trace);
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            var configMenu = this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>
                ("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: this.ModManifest,
                reset: () => this.Config = new ModConfig(),
                save: () =>
                {
                    this.Helper.WriteConfig(this.Config);
                    Monitor.Log($"Config saved: StartingMoney = {this.Config.StartingMoney}g", LogLevel.Info);
                }
            );

            configMenu.AddTextOption(
                mod: this.ModManifest,
                name: () => this.Helper.Translation.Get("config.startingMoney.name"),
                tooltip: () => this.Helper.Translation.Get("config.startingMoney.tooltip"),
                getValue: () => this.Config.StartingMoney.ToString(),
                setValue: value => this.Config.StartingMoney = int.TryParse(value, out int result) && result >= 0 ? result : this.Config.StartingMoney
            );
        }

        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            if (Game1.stats.DaysPlayed == 1)
            {
                Game1.player.Money = Config.StartingMoney;
                Game1.player.totalMoneyEarned = 0;
                Monitor.Log($"Applied starting money: {Config.StartingMoney}g", LogLevel.Info);
            }
        }

    }
}
