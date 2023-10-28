using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;


    public void Spawn()
    {
        var mazeGenerator = new MazeGenerator(mazeWidth, mazeHeight);
        var labyrinths = mazeGenerator.Generate();

        for (int i = 0; i < labyrinths.GetLength(0); i++)
        {
            for (int j = 0; j < labyrinths.GetLength(1); j++)
            {
                CellWallsCollector cell = Instantiate(cellPrefab, new Vector2(i - (mazeWidth/2) + 0.5f, j - (mazeHeight/2) + 0.5f), Quaternion.identity).GetComponent<CellWallsCollector>();

                if (i == 0 && j == 0)
                {
                    cell.bottomWallSpriteRenderer.color = Color.green;
                    cell.leftWallSpriteRenderer.color = Color.green;
                }

                if (i == (mazeWidth/2) - 1 && j == (mazeHeight/2) - 1)
                {
                    cell.bottomWallSpriteRenderer.color = Color.green;
                    cell.leftWallSpriteRenderer.color = Color.green;
                }

                cell.leftWall.SetActive(labyrinths[i, j].isHaveLeftWall);
                cell.bottomWall.SetActive(labyrinths[i, j].isHaveBottomtWall);
            }
        }
    }
}
