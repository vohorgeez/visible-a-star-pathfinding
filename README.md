# Visible A* Pathfinding - Unity 2D Demo

Interactive 2D visualization of the **A\*** pathfinding algorithm on a grid, with terrain costs, obstacles, and real-time interaction.
The project includes a **tested algorithmic core** (A\* compared to Dijkstra) and a **Unity 2D demo** to make the behavior observable.

---

## Features (v1)

- A* pathfinding on a 2D grid (4 directions)
- Terrain weights (variable movement cost)
- Obstacles (blocked cells)
- Path reconstruction
- Real-time interaction in Unity:
    - Move start and goal with the mouse
    - Path recomputed instantly
- Algorithm validated against Dijkstra (unit tests)

---

## Algorithms & Concepts

- **A\*** with admissible Manhattan heuristic
- **Dijkstra** used as optimal reference
- Grid graph representation
- Cost-aware shortest path search
- Separation between:
    - pure algorithmic logic
    - rendering / interaction layer

---

## Tech Stack

- **C#**
- **.NET** (algorithm + unit test)
- **Unity 2D** (visual demo)
- Custom minimal priority queue (Unity-compatible)

---

## Project structure

.
├── GridBasics/ # Algorithm + unit tests (.NET)
│ ├── Grid.cs
│ ├── AStarPathfinder.cs
│ ├── DijkstraPathfinder.cs
│ └── GridBasics.Tests/
│
├── unity/
│ └── VisibleAStarPathfinding/
│ ├── Assets/
│ │ └── Scripts/Pathfinding/
│ │ ├── Grid.cs
│ │ ├── AStarPathFinder.cs
│ │ ├── MinPriorityQueue.cs
│ │ └── GridView.cs
│ ├── Packages/
│ └── ProjectSettings/
│
└── README.md

---

## How to run (Unity demo)

1. Install **Unity Hub**
2. Open the project located at: `unity/VisibleAStarPathfinding`
3. Open `SampleScene`
4. Press **Play**

---

## Controls

- **Left click**: move the **Start** cell
- **Shift + Left click**: move the **Goal** cell
- The path updates automatically

---

## Tests

The algorithm is tested independently of Unity:
- shortest path correctness
- no-path scenarios
- terrain cost handling
- A* optimality validated against Dijkstra

Run tests from the root:
```bash
dotnet test
```

---

## Possible Extensions

- Visualize open / closed sets
- Add diagonal movement (8-direction)
- Place walls interactively
- Dynamic obstacles
- Tilemap-based rendering
- Performance comparison A* vs Dijkstra

## Purpose

This project is designed as:
- an educational visualization of pathfinding algorithms
- a portfolio-ready demo showing both algorithmic rigor and practical integration in a game engine.