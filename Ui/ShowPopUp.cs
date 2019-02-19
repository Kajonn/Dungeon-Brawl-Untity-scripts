using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace dungeonbrawl.UI
{
    public class ShowPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public GameObject popUpBox;

        private GameObject instantiatedPopUpBox;

        private string popupText;

        public string PopupText
        {
            get => popupText;
            set
            {
                popupText = value;
                UpdatePopUpText();
            }
        }

        void SetPopUpText(string text)
        {
            PopupText = text;
        }

        private void OnDestroy()
        {
            RemovePopUp();
        }

        private void UpdatePopUpText()
        {
            if (instantiatedPopUpBox != null)
            {
                instantiatedPopUpBox.GetComponentInChildren<Text>().text = popupText;
            }
        }

        private void RemovePopUp()
        {
            if (instantiatedPopUpBox != null)
            {
                Destroy(instantiatedPopUpBox);
                instantiatedPopUpBox = null;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RemovePopUp();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (instantiatedPopUpBox == null && popupText != "")
            {
                instantiatedPopUpBox = Instantiate(popUpBox);
                var rootCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
                instantiatedPopUpBox.transform.SetParent(rootCanvas.transform);
                var myCanvas = rootCanvas.GetComponentInParent<Canvas>();

                //Vector2 pos;
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);

                var boxpos = transform.position;// myCanvas.transform.TransformPoint(pos);
                                                //boxpos.x += 50;

                instantiatedPopUpBox.transform.position = boxpos;
                UpdatePopUpText();
            }
        }

    }
}