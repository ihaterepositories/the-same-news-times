using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonControllers
{
    public class NavigationButtonsGroup : MonoBehaviour
    {
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private Color selectedButtonColor;
        [SerializeField] private Color defaultButtonColor;
        [SerializeField] private float selectionCooldown = 0.2f;
        [SerializeField] private List<Image> buttonImages;
        [SerializeField] private List<RectTransform> buttonIconsRects;

        private Button[] buttons;
        private int selectedIndex;
        private float lastSelectionTime;
        
        private Vector2 _originalButtonScale;

        private void Start()
        {
            buttons = buttonsParent.GetComponentsInChildren<Button>();
            _originalButtonScale = buttons[0].transform.localScale;
            SelectButton(selectedIndex);
        }

        private void Update()
        {
            ProcessScrolling();
            ProcessSelection();
        }

        private void ProcessSelection()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Time.time - lastSelectionTime > selectionCooldown)
                {
                    buttons[selectedIndex].onClick.Invoke();
                    lastSelectionTime = Time.time;
                }
            }
        }

        private void ProcessScrolling()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SelectButton(selectedIndex - 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectButton(selectedIndex + 1);
            }
        }

        private void SelectButton(int index)
        {
            index = Mathf.Clamp(index, 0, buttons.Length - 1);

            RotateIcon(index, Random.Range(0, 3));
            
            buttons[selectedIndex].GetComponentInChildren<RectTransform>().DOScale(_originalButtonScale, 0.5f);
            buttons[index].GetComponentInChildren<RectTransform>().DOScale(_originalButtonScale * 1.25f, 0.5f);
            
            buttonImages[selectedIndex].color = defaultButtonColor;
            buttonImages[index].color = selectedButtonColor;

            selectedIndex = index;
        }
        
        private void RotateIcon(int index, int rotationMode)
        {
            switch (rotationMode)
            {
                case 0: buttonIconsRects[index].DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360);
                    break;
                case 1: buttonIconsRects[index].DORotate(new Vector3(0f, 360f, 0f), 0.5f, RotateMode.FastBeyond360);
                    break;
                case 2: buttonIconsRects[index].DORotate(new Vector3(360f, 0f, 0f), 0.5f, RotateMode.FastBeyond360);
                    break;
            }
        }
    }
}
