using System.Collections.Generic;
using UnityEngine;

public class PositionBlockController : MonoBehaviour
{
    private static List<int> positionsX;
    private static List<int> positionsY;

    private void Awake()
    {
        positionsX = new List<int>();
        positionsY = new List<int>();
    }

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += ClearBlockedPositions;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= ClearBlockedPositions;
    }

    private void ClearBlockedPositions()
    {
        positionsX.Clear();
        positionsY.Clear();
    }

    public static void BlockPosition(int x, int y, bool isBlockNearestPositions)
    {
        positionsX.Add(x);
        positionsY.Add(y);
   
        if (isBlockNearestPositions )
        {
            positionsX.Add(x + 1);
            positionsX.Add(x - 1);
            positionsY.Add(y + 1);
            positionsY.Add(y - 1);
        }
    }

    public static bool CheckPositionAvailability(int x, int y)
    {
        for (int i = 0; i < positionsX.Count; i++)
        {
            if (positionsX[i] == x)
            {
                for (int j = 0; j < positionsY.Count; j++)
                {
                    if (positionsY[j] == y) return false;
                }
            }
        }

        return true;
    }
}
