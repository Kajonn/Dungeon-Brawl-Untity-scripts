using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dungeonbrawl.Common;

namespace dungeonbrawl.UI
{
    public class EquipmentDisplayer : MonoBehaviour
    {
        public GameObject popUpBox;

        public GameObject rightHandButton;
        public GameObject leftHandButton;
        public GameObject headButton;
        public GameObject chestButton;
        public GameObject legsButton;
        public GameObject ringButton;
        public GameObject amulettButton;

        private EquipmentHolder equipmentHolder;

        // Use this for initialization
        void Start()
        {
            equipmentHolder = GetComponent<EquipmentHolder>();
            EquipmentUpdated();
        }

        void SetButton(GameObject button, EquipmentSlot slot)
        {
            if (button.GetComponent<ShowPopUp>() == null) {
                button.AddComponent(typeof(ShowPopUp));
                button.GetComponent<ShowPopUp>().popUpBox = popUpBox;
            }

            var equipment = equipmentHolder.GetEquipment(slot);
            if (equipment == null)
            {
                button.GetComponentInChildren<Text>().name = "";
                button.GetComponent<Image>().sprite = null;
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<ShowPopUp>().PopupText = "";
            }
            else
            {
                button.GetComponentInChildren<Text>().text = equipment.GetComponent<Item>().itemName;
                button.GetComponent<Image>().sprite = equipment.GetComponent<SpriteRenderer>().sprite;
                button.GetComponent<Button>().onClick.AddListener(delegate { equipmentHolder.UnEquip(slot); });
                button.GetComponent<ShowPopUp>().PopupText = equipment.GetComponent<Item>().GetFullDescription();
            }


        }

        void EquipmentUpdated()
        {
            if (equipmentHolder == null)
                return;

            if (rightHandButton != null)
            {
                SetButton(rightHandButton, EquipmentSlot.RightHand);
            }
            if (leftHandButton != null)
            {
                SetButton(leftHandButton, EquipmentSlot.LeftHand);
            }
            if (headButton != null)
            {
                SetButton(headButton, EquipmentSlot.Head);
            }
            if (chestButton != null)
            {
                SetButton(chestButton, EquipmentSlot.Chest);
            }
            if (legsButton != null)
            {
                SetButton(legsButton, EquipmentSlot.Legs);
            }
            if (ringButton != null)
            {
                SetButton(ringButton, EquipmentSlot.Ring);
            }
            if (amulettButton != null)
            {
                SetButton(amulettButton, EquipmentSlot.Amulett);
            }

        }
    }
}