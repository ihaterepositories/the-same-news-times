using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MazeGeneration;
using UnityEngine;

namespace Animations
{
    public class MazeDisappearanceAnimation : MonoBehaviour
    {
        public void PlayForInvisibleLevel(List<CellWallsCollector> cells)
        {
            StartCoroutine(DisappearancesAnimationCoroutine(cells));
        }

        private IEnumerator DisappearancesAnimationCoroutine(List<CellWallsCollector> cells)
        {
            yield return new WaitForSeconds(2.5f);

            foreach (var cell in cells)
            {
                cell.bottomWallSpriteRenderer.DOFade(0, 1f);
                cell.leftWallSpriteRenderer.DOFade(0, 1f);
            }
        }
    }
}
