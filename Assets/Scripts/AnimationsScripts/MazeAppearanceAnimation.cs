using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Models.MazeGeneration;
using UnityEngine;

public class MazeAppearanceAnimation : MonoBehaviour
{
    public void Play(List<CellWallsCollector> cells)
    {
        ShuffleCells(cells);
        StartCoroutine(AppearanceAnimationCoroutine(cells));
    }

    public void PlayForInvisibleLevel(List<CellWallsCollector> cells)
    {
        ShuffleCells(cells);
        StartCoroutine(AppearanceAnimationCoroutine(cells));
        StartCoroutine(DissappearanceAnimationCoroutine(cells));
    }

    private void ShuffleCells(List<CellWallsCollector> cells)
    {
        System.Random rng = new System.Random();
        int n = cells.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CellWallsCollector value = cells[k];
            cells[k] = cells[n];
            cells[n] = value;
        }
    }

    private IEnumerator AppearanceAnimationCoroutine(List<CellWallsCollector> cells)
    {
        foreach (var cell in cells)
        {
            cell.PlayAppearanceAnimation(0.5f);
            yield return new WaitForSeconds(0.0001f);
        }
    }

    private IEnumerator DissappearanceAnimationCoroutine(List<CellWallsCollector> cells)
    {
        yield return new WaitForSeconds(4f);

        foreach (var cell in cells)
        {
            cell.bottomWallSpriteRenderer.DOFade(0, 1f);
            cell.leftWallSpriteRenderer.DOFade(0, 1f);
        }
    }
}
