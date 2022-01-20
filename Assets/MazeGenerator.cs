using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeGenerator
{
    public int WightX = InputData.WightX + 1;
    public int WightY = InputData.WightY + 1;

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[WightX, WightY];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {      
                maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, WightY - 1].WallVertical = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[WightX - 1, y].WallHorizontal = false;
        }
        RemoveWallsWithBacktracker(maze);
        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < WightX - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < WightY - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }
    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallHorizontal = false;
            else b.WallHorizontal = false;
        }
        else
        {
            if (a.X > b.X) a.WallVertical = false;
            else b.WallVertical = false;
        }
    }
}
