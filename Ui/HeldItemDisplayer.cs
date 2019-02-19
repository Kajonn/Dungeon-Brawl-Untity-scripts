using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dungeonbrawl;
using dungeonbrawl.Common;

namespace dungeonbrawl.UI
{
    public class HeldItemDisplayer : MonoBehaviour
    {

        public GameObject buttonPrefab;
        public GameObject listPanel;

        private ItemHolder itemHolder;
        private ObjectPool objectPool;


        // Use this for initialization
        void Start()
        {
            itemHolder = GetComponent<ItemHolder>();
            objectPool = FindObjectOfType<ObjectPool>();
        }

        GameObject createNewImage()
        {

            var panel = new GameObject();
            panel.AddComponent<CanvasRenderer>();
            panel.AddComponent<RectTransform>();
            panel.AddComponent<Image>();
            panel.AddComponent<LayoutElement>();
            panel.GetComponent<LayoutElement>().preferredHeight = 50;

            return panel;
        }

        void ItemsUpdated()
        {
            if (itemHolder != null)
            {
                foreach (Transform child in listPanel.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                foreach (var item in itemHolder.Items)
                {
                    var buttonObject = objectPool.GetFreeInstance(buttonPrefab);

                    SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
                    buttonObject.GetComponent<Image>().sprite = spriteRenderer.sprite;

                    if (item.usesLeft > 1)
                    {
                        buttonObject.GetComponentInChildren<Text>().text = item.usesLeft.ToString();
                    }
                    else
                    {
                        buttonObject.GetComponentInChildren<Text>().text = "";
                    }

                    var equipment = item.GetComponent<Equipment>();
                    var button = buttonObject.GetComponent<Button>();
                    if (equipment != null)
                    {
                        var equipmentHolder = itemHolder.GetComponent<EquipmentHolder>();
                        if (equipmentHolder != null)
                        {
                            button.onClick.AddListener(
                                delegate { equipmentHolder.Equip(equipment, equipment.equipmentSlot); }
                                );
                        }
                    }
                    else if (item.isUsable)
                    {
                        button.onClick.AddListener(
                                delegate
                                {
                                    item.Use(itemHolder.gameObject);
                                    this.ItemsUpdated();
                                }
                                );
                    }

                    buttonObject.transform.SetParent(listPanel.transform, false);
                    
                    buttonObject.GetComponent<ShowPopUp>().PopupText = item.GetFullDescription();

                }
            }
        }

    }
}