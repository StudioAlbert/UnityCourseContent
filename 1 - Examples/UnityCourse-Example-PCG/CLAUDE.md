# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 6 (6000.3.10f1) teaching project demonstrating Procedural Content Generation (PCG) algorithms. Uses Universal Render Pipeline with 2D tilemaps. This is a course example project — clarity and pedagogical value matter.

## Development

This is a Unity project — there are no CLI build/test commands. All development happens through the Unity Editor:

- **Open project:** Unity Hub → Open → select this folder
- **Run tests:** Unity Editor → Window → General → Test Runner
- **Trigger dungeon generation in-editor (without Play mode):** Select `DungeonGenerator` GameObject → Inspector → "Generate" button (via `DungeonGeneratorEditor.cs` custom editor)

## Architecture

### PCG Systems (`Assets/Scripts/PCG/`)

Two independent dungeon generation pipelines:

**BSP Tree Pipeline** — main active system:
- `BSPTree.cs` — static recursive partitioner; splits a `BoundsInt` using configurable `minCutRatio`/`maxCutRatio` until leaf size < `maxSurface`
- `BspNode.cs` — tree node holding bounds, room rect (shrunk by `shrinkFactor` + `jitter`), and child nodes
- `BspCorridor.cs` — data class representing an L-shaped corridor between two room centers
- `BspCorridorsUnionFind.cs` — Union-Find (disjoint set) structure used to build MST
- `CorridorsKruskalFilter.cs` — Kruskal's algorithm to select minimum spanning tree corridors from all possible connections
- `BspDungeonGenerator.cs` — MonoBehaviour orchestrator; calls BSPTree → extracts rooms → runs Kruskal filter → paints tilemaps

**Random Walk Pipeline:**
- `RandomWalkGenerator.cs` — static generator; takes random steps (Von Neumann directions), places rooms at waypoints
- `RandomWalk.cs` — MonoBehaviour orchestrator for the random walk scene

**Shared utilities (`Utils.cs`, `TilemapUtils.cs`):**
- Direction arrays: `VonNeumann` (4-dir), `Moore` (8-dir)
- `ManhattanDistance()`
- Tilemap painting helpers

### Room System (`Assets/Scripts/Rooms/`)

- `RoomStereotype.cs` — ScriptableObject defining a room type (Fight/Shop/Boss/End/Pool/Secret) with weighted links to other stereotypes. `GetNextStereotype()` performs weighted random selection. Assets live in `Assets/Prefabs/Rooms/`.
- `RoomChain.cs` — MonoBehaviour on room triggers; fires transition to next room on collider enter

### Markov / Enemy Systems (`Assets/Scripts/`)

- `MarkovState.cs` / `MarkovChain.cs` — generic probabilistic state machine; `WeatherChain` is the example (Sunny↔Rainy)
- `EnnemyMarkovState.cs` — ScriptableObject combining Markov transitions with enemy stats (`atkRate`, `defRate`, `hpMax`, menace points, prefab reference)
- `Spawner.cs` — stub; menace-budget-based entity spawner (incomplete)

### Key Architectural Patterns

- **Static generation methods** — core algorithms (`BSPTree`, `RandomWalkGenerator`) are static and return data structures, not MonoBehaviours
- **MonoBehaviour orchestrators** — thin wrappers (`BspDungeonGenerator`, `RandomWalk`) own tilemaps, call static generators, paint results
- **Gizmo debugging** — all generators draw rooms/corridors/cuts via `OnDrawGizmos` for editor visualization without Play mode
- **ScriptableObject assets** — room definitions and enemy configs are `.asset` files, not scene-embedded data

### Scenes

| Scene | Purpose |
|---|---|
| `BspDungeonGeneration.unity` | BSP tree dungeon demo |
| `RandomWalk.unity` | Random walk dungeon demo |
| `DungeonFight.unity` | Room chain + enemy spawning demo |
| `SampleScene.unity` | Scratch/test scene |
