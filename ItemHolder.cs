using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl
{
    public class ItemHolder : MonoBehaviour
    {

        public List<Item> Items = new List<Item>();

        private ObjectPool objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();
            List<Item> itemsToAdd = new List<Item>();
            List<Item> itemsToRemove = new List<Item>();
            foreach (Item item in Items)
            {
                if (Common.Utils.IsPrefab(item.gameObject))
                {
                    itemsToAdd.Add(objectPool.GetFreeInstance(item.gameObject).GetComponent<Item>());
                    itemsToRemove.Add(item);
                }
            }
            foreach (Item item in itemsToRemove)
            {
                Items.Remove(item);
            }
            foreach (Item item in itemsToAdd)
            {
                Items.Add(item);
            }
            foreach (Item item in Items)
            {
                item.Pickup(this);
            }
        }

        public void ItemDestroyed(Item item)
        {
            Debug.Log("Removing destroyed item from itemHolder: " + item.name);
            Items.Remove(item);

            SendMessage("ItemsUpdated", SendMessageOptions.DontRequireReceiver);
        }

        public void Pickup(Item item)
        {
            Debug.Log("Pickup" + item.name);

            if (item.destroyWhenUsed)
            {
                //Check if item type allready exists in list
                var found = Items.Find((localitem => localitem.Equals(item)));
                if (found != null)
                {
                    Debug.Log("Stacking item");
                    item.StackWith(found);
                    SendMessage("ItemsUpdated", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            }

            Debug.Log("Item picked up");
            Items.Add(item);
            item.Pickup(this);
            SendMessage("ItemsUpdated", SendMessageOptions.DontRequireReceiver);
        }

        public void Drop(Item item, Vector3 pos)
        {
            Debug.Log("Drop " + item.name + " at " + pos.ToString());

            Items.Remove(item);
            item.gameObject.transform.position = pos;
            item.Drop();

            Debug.Log("Item " + item.name + " dropped");

            SendMessage("ItemsUpdated", SendMessageOptions.DontRequireReceiver);
        }

        public void DropAllItems(Vector3 pos)
        {
            List < Item > dropItem = new List<Item>(Items);
            foreach (Item item in dropItem)
            {
                Drop(item, pos);
            }
        }

    }
}