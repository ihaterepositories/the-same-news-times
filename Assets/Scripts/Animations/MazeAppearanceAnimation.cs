using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MazeGeneration;
using UnityEngine;

namespace Animations
{
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
            StartCoroutine(DisappearancesAnimationCoroutine(cells));
        }

        private void ShuffleCells(List<CellWallsCollector> cells)
        {
            var rng = new System.Random();
            var n = cells.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (cells[k], cells[n]) = (cells[n], cells[k]);
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

        private IEnumerator DisappearancesAnimationCoroutine(List<CellWallsCollector> cells)
        {
            yield return new WaitForSeconds(4f);

            foreach (var cell in cells)
            {
                cell.bottomWallSpriteRenderer.DOFade(0, 1f);
                cell.leftWallSpriteRenderer.DOFade(0, 1f);
            }
        }
    }
}
