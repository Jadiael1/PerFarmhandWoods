# Per Farmhand Woods

Per Farmhand Woods is a Stardew Valley mod that gives every multiplayer guest
their own instanced Secret Woods. Hosts keep the vanilla clearing and visitors
are seamlessly redirected into their personal map, so no one has to race for
hardwood spawns or forage drops.

## Why this mod exists

Stardew Valley was originally balanced around a single farmer. Even with co-op
added later, a lot of the world remains shared. In split-wallet runs that
sharing becomes friction: hardwood is a fixed resource, the Secret Woods only
spawns six stumps per day, and guests often miss out. Watching friends sprint
to the clearing every morning was the motivation to carve out a dedicated woods
for each player.

## Core features

- Dedicated Secret Woods per guest, created on demand and reused across saves.
- Forest warp tiles now run a custom touch action that redirects non-host
  players into their personal woods while preserving the destination tile.
- Guests that step into the shared Woods map are automatically bounced into
  their private instance even if another mod triggers a direct warp.
- Woods ownership is saved, restored, and cleaned up across sessions to avoid
  orphaned locations.

## How it works

- The mod listens to asset requests for `Maps/Forest*` and injects a custom
  `TouchAction` (`DerexSV.PerFarmhandWoods.EnterWoods`) onto the vanilla warp
  tiles that lead into the woods.
- That touch action checks the visiting farmer and warps them into
  `Woods_<PlayerID>`, creating the map on the fly when needed.
- Lifecycle events (`SaveLoaded`, `Saving`, `LoadStageChanged`, etc.) ensure the
  instances are registered with the game, saved into mod data, and cleaned up
  when returning to the title screen.

## Installation

1. Install the latest version of [SMAPI](https://smapi.io/) (4.3.2 or higher).
2. Download the latest release of this mod.
3. Unzip and drop the `PerFarmhandWoods` folder into your `Stardew Valley/Mods`
   directory.
4. Launch Stardew Valley through SMAPI.

### Build from source

```bash
dotnet build PerFarmhandWoods/PerFarmhandWoods.csproj
```

The build uses the [Mod Build Config](https://github.com/Pathoschild/SMAPI-mod-build-config)
package to deploy the compiled mod straight into the game’s `Mods` folder.

## Compatibility

- **SMAPI**: Requires SMAPI 4.3.2+ (matches `manifest.json`).
- **Game version**: Developed against Stardew Valley 1.6.x.
- **Map/warp edits**: Mods that rewrite `Maps/Forest*` warp tiles or replace the
  Secret Woods entirely may conflict. The asset patch runs with late priority,
  so most content packs should coexist, but verify in-game.
- **Custom warps**: Anything that teleports guests directly into the shared
  `Woods` map is intercepted by the Warped event handler and redirected, so the
  experience remains consistent.
- **Single player**: No gameplay changes occur; the host keeps the default map.

## Known limitations

- The host still shares the vanilla Secret Woods with farmhands if they follow
  the host through the exit warp. Guests can re-enter their instance via the
  forest warp tiles.
- Mods that fundamentally replace the woods’ map data may require manual patch
  adjustments.

## Troubleshooting

- Guests looping between maps generally means another mod removed the injected
  touch action. Check SMAPI’s log for warnings from Per Farmhand Woods.
- If a woods instance ever fails to spawn, remove the mod data entry
  `derexsv.pfw.locations` (*.json) from the affected save and reload. The mod
  will rebuild instances automatically on the next day start or warp.

## Credits & thanks

- **DerexSV** – original concept and implementation.
- SMAPI community and Pathoschild for the modding toolkit and build config.
- Friends who kept losing hardwood races and inspired the feature set.
