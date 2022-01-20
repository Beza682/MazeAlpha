using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallVertical = true;
    public bool WallHorizontal = true;

    public bool Visited = false;
}
