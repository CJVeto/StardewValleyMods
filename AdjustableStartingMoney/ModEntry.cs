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

            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;

            helper.ConsoleCommands.Add("asm_show", "Shows the configured starting money amount.", OnShowCommand);
            helper.ConsoleCommands.Add("asm_set", "Sets the starting money amount. Usage: asm_set <amount>", OnSetCommand);

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
                    Monitor.Log(Helper.Translation.Get("command.set.saved", new { value = this.Config.StartingMoney }), LogLevel.Info);
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
            if (Game1.stats.DaysPlayed == 1 && Context.IsMainPlayer)
            {
                Game1.player.Money = Config.StartingMoney;
                //Setting total money earned to 0 as our changes would update both Current Funds and Total Earnings otherwise
                Game1.player.totalMoneyEarned = 0;
                Monitor.Log($"Applied starting money: {Config.StartingMoney}g", LogLevel.Info);
            }
        }

        private void OnShowCommand(string command, string[] args)
        {
            Monitor.Log(Helper.Translation.Get("command.show.message", new { value = Config.StartingMoney }), LogLevel.Info);
        }

        private void OnSetCommand(string command, string[] args)
        {
            if (args.Length == 0 || !int.TryParse(args[0], out int amount) || amount < 0)
            {
                Monitor.Log(Helper.Translation.Get("command.set.invalid"), LogLevel.Warn);
                return;
            }

            Config.StartingMoney = amount;
            Helper.WriteConfig(Config);
            Monitor.Log(Helper.Translation.Get("command.set.success", new { value = amount }), LogLevel.Info);
        }
    }
}
