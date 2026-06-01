# Adjustable Starting Money

A Stardew Valley mod that lets you configure how much money you start with on a new farm.

## Features
- Set your starting money to any non-negative value
- Defaults to 500g (vanilla behavior)
- Optional [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) support
- Console commands for quick adjustments
- Only applies on Spring 1 Year 1 of a new save — existing saves are unaffected

## Requirements
- [SMAPI](https://smapi.io/) 4.0.0 or later
- Stardew Valley 1.6 or later
- [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) (optional)

## Installation
1. Install [SMAPI](https://smapi.io/)
2. Download the mod and extract the `AdjustableStartingMoney` folder into your `Mods` folder
3. Run the game through SMAPI

## Configuration
Edit `config.json` in the mod folder, or use Generic Mod Config Menu if installed.

| Field | Default | Description |
|-------|---------|-------------|
| `StartingMoney` | `500` | The amount of gold you start with on a new save. Must be 0 or a positive integer. |

## Console Commands
| Command | Description |
|---------|-------------|
| `asm_show` | Displays the current configured starting money amount |
| `asm_set <amount>` | Sets the starting money to the specified amount |

## Notes
- Starting money applies on save load when it is Spring 1 Year 1. If you start a new save, quit without saving, and reload, the configured amount will be applied again on reload.
- The save file selection screen will always show 500g for a new save before it is first loaded. The correct amount is applied once the save loads.
- Total Earnings is reset to 0 on a new save. This is intentional — starting money is not considered earned income.
- Multiplayer: Only the host's starting money is adjusted. Farmhand starting money is not modified by this mod.

## Source Code
[GitHub](https://github.com/CJVeto/StardewValleyMods)