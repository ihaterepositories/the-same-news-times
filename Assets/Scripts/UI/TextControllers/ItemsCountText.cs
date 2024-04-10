using Controllers.InGameControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TextControllers
{
    public class ItemsCountText : MonoBehaviour
    {
        [SerializeField] private Text itemsCountText;
        [SerializeField] private Inventory inventory;

        private void Update()
        {
            SetItemsCountText();
        }

        private void SetItemsCountText()
        {
            itemsCountText.text = inventory.LifeSaversCount + "x" + "\n" +
                                  inventory.BoostersCount + "x";
        }
    }
}