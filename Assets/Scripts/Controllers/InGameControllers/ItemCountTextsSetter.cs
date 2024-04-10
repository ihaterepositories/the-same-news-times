using UI.TextControllers;
using UnityEngine;
using Zenject;

namespace Controllers.InGameControllers
{
    public class ItemCountTextsSetter : MonoBehaviour
    {
        [SerializeField] private ItemCountText lifeSaversCountText;
        [SerializeField] private ItemCountText boostersCountText;
        
        private Inventory _inventory;
        
        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void Start()
        {
            lifeSaversCountText.GetCount = () => _inventory.LifeSaversCount;
            boostersCountText.GetCount = () => _inventory.BoostersCount;
        }
    }
}