using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace VetoCV.AdjustableStartingMoney
{
    internal class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        }

        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
        }

    }
}
