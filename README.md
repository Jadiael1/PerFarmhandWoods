*[**English**](README.md) · [Português (Brasil)](README.pt-BR.md)*

# Per Farmhand Woods

Per Farmhand Woods is a Stardew Valley mod that gives every multiplayer guest
their own instanced `Secret Woods`. The host keeps the teleport to the original
`Secret Woods`, and visitors are seamlessly redirected into their personal map,
so no one has to sprint for hardwood or forage drops.

## Why this mod exists

Stardew Valley was originally balanced around a single farmer. Even with co-op
added later, much of the world remains shared. In split-wallet multiplayer that
becomes friction: hardwood is a limited resource, the `Secret Woods` only spawns
six stumps per day, and guests often leave empty-handed. Watching friends race to
the `Secret Woods` every morning inspired carving out a dedicated `Secret Woods`
for each player.

## Core features

- Guest-exclusive Secret Woods, created on demand and reused across saves.
- Forest warp tiles use a custom `TouchAction` that redirects non-host players to
  their personal woods while preserving the destination tile.
- Guests who step into the shared Woods map are automatically routed into their
  private instance.
- Woods ownership is saved, restored, and cleaned up between sessions to avoid
  orphaned locations.

## How it works

- The mod listens for asset requests to `Maps/Forest*` and injects a custom
  `TouchAction` (`DerexSV.PerFarmhandWoods.EnterWoods`) on the vanilla warp tiles
  that lead into the woods.
- That action checks the visiting farmer and warps them to `Woods_<PlayerID>`,
  creating the map automatically whenever needed.
- Lifecycle events (`SaveLoaded`, `Saving`, `LoadStageChanged`, etc.) make sure
  instances are registered with the game, saved into mod data, and cleared when
  returning to the title screen.

## Installation

1. Install the latest version of [SMAPI](https://smapi.io/) (4.3.2 or newer).
2. Download the latest release of this mod.
3. Extract and drop the `PerFarmhandWoods` folder into the Stardew Valley `Mods`
   directory.
4. Launch Stardew Valley through SMAPI.

### Build from source

```bash
dotnet build PerFarmhandWoods.sln -c release
```

The build uses the [Mod Build Config](https://www.nuget.org/packages/Pathoschild.Stardew.ModBuildConfig)
package, which also deploys the mod directly into the game’s `Mods` folder.

## Compatibility

- **SMAPI**: Requires SMAPI 4.3.2+ (matches `manifest.json`).
- **Game version**: Developed against Stardew Valley 1.6.x.
- **Map/warp edits**: Mods that rewrite `Maps/Forest*` warp tiles or entirely
  replace the `Secret Woods` may conflict. The patch runs late, so most content
  packs should coexist, but verify in-game.
- **Custom warps**: Anything that teleports guests directly into the shared
  `Woods` map is intercepted by the `Warped` handler and redirected, keeping the
  experience consistent (Currently disabled).
- **Single player**: No gameplay changes; the host keeps the default map.

## Known limitations

- The host still shares the default `Secret Woods` with farmhands if they follow
  through the exit warp. Guests can re-enter their instance using the forest warp
  tiles.
- Mods that deeply replace the woods map data might need custom patch tweaks.

## Contribution and redistribution policy

- Forks or redistributions that alter the mod to release “alternate editions” are
  not authorized. The goal is to keep a single official version derived from this
  codebase.
- Contributions are welcome, but any improvement or new feature must be aligned in
  advance via issues (or another official repository channel) before development
  begins.
- Open an issue describing the proposal, wait for alignment, and only then spend
  time on the code to avoid frustration with rejected pull requests.

## Credits & thanks

- **DerexSV** – original concept and implementation.
- SMAPI community and Pathoschild for the modding toolkit and build config.
- Friends who kept losing the hardwood race and inspired the feature set.
