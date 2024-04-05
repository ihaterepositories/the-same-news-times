using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonControllers
{
    public class ButtonsListController : MonoBehaviour
    {
        [SerializeField] private KeyCode upKey;
        [SerializeField] private KeyCode downKey;
        [SerializeField] private KeyCode clickKey;
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private Color selectedButtonColor;
        [SerializeField] private Color defaultButtonColor;
        [SerializeField] private float selectionCooldown = 0.2f;

        private Button[] buttons;
        private int selectedIndex = 0;
        private float lastSelectionTime;

        private void Start()
        {
            buttons = buttonsParent.GetComponentsInChildren<Button>();
            SelectButton(selectedIndex);
        }

        public void Update()
        {
            if (Input.GetKeyDown(upKey))
            {
                SelectButton(selectedIndex - 1);
            }
            else if (Input.GetKeyDown(downKey))
            {
                SelectButton(selectedIndex + 1);
            }
            
            if (Input.GetKeyDown(clickKey))
            {
                if (Time.time - lastSelectionTime > selectionCooldown)
                {
                    Button selectedButton = buttons[selectedIndex];
                    
                    selectedButton.onClick.Invoke();
                    
                    lastSelectionTime = Time.time;
                }
            }
        }

        private void SelectButton(int index)
        {
            index = Mathf.Clamp(index, 0, buttons.Length - 1);

            buttons[selectedIndex].GetComponentInChildren<Text>().color = defaultButtonColor;
            buttons[index].GetComponentInChildren<Text>().color = selectedButtonColor;

            selectedIndex = index;
        }
    }
}
