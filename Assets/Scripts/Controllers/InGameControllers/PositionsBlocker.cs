using System.Collections.Generic;
using UnityEngine;

namespace Controllers.InGameControllers
{
    public class PositionsBlocker : MonoBehaviour
    {
        private static List<int> _positionsX;
        private static List<int> _positionsY;

        private void Awake()
        {
            _positionsX = new List<int>();
            _positionsY = new List<int>();
        }

        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += ClearBlockedPositions;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= ClearBlockedPositions;
        }

        private void ClearBlockedPositions()
        {
            _positionsX.Clear();
            _positionsY.Clear();
        }

        public void BlockPosition(int x, int y, bool isBlockNearestPositions)
        {
            _positionsX.Add(x);
            _positionsY.Add(y);
   
            if (isBlockNearestPositions )
            {
                _positionsX.Add(x + 1);
                _positionsX.Add(x - 1);
                _positionsY.Add(y + 1);
                _positionsY.Add(y - 1);
            }
        }

        public bool CheckPositionAvailability(int x, int y)
        {
            for (var i = 0; i < _positionsX.Count; i++)
            {
                if (_positionsX[i] == x)
                {
                    for (var j = 0; j < _positionsY.Count; j++)
                    {
                        if (_positionsY[j] == y) return false;
                    }
                }
            }

            return true;
        }
    }
}
