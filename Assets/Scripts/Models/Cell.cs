using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int x;
    public int y;
    public int distanceFromStartPoint;

    public bool isHaveLeftWall = true;
    public bool isHaveBottomtWall = true;
    public bool isVisitedByGenerator = false;
}
