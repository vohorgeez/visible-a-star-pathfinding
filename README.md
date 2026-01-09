# Visible A* Pathfinding

## Overview
2D tilemap pathfinding demo with a fully visible A* algorithm.
The project focuses on correctness, testability, and algorithmic clarity.

## Features (v1)
- Grid-based pathfinding (4-directional)
- A* with admissible heuristic (Manhattan)
- Path reconstruction
- Visualization of:
  - visited nodes
  - frontier (open set)
  - final path
- Configurable terrain costs and obstacles

## Technical Focus
- Graph-based grid representation
- Unit-tested pathfinding logic
- Heuristic admissibility verification
- Separation between algorithm and rendering

## Tests
- Graph connectivity
- Shortest path on toy cases
- A* vs Dijkstra equivalence
- Heuristic <= real path cost

## Tech Stack
- Unity
- C#
- NUnit (EditMode tests)

## Roadmap
- [X] Grid representation
- [ ] A* core
- [ ] Visualization
- [ ] Dynamic obstacle avoidance
