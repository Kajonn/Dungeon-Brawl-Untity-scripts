using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    //Item does not use object pool as it will be disabled when picked up
    public class Item : MonoBehaviour
    {

        public string itemName;
        public string itemDescription;
        public bool isUsable = false;
        public List<StatusEffect> effectsWhenUsed;
        public bool destroyWhenUsed = false;
        public int usesLeft;

        private ItemHolder pickedUpBy;
        private ObjectPool objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();
        }
        
        private void OnDestroy()
        {
            Debug.Log("Destroying item");

            if (pickedUpBy != null)
            {
                pickedUpBy.ItemDestroyed(this);
            }
        }

        public bool Equals(Item otherItem)
        {
            return otherItem.itemName.Equals(itemName) &&
                   otherItem.itemDescription.Equals(itemDescription);
        }

        public void Pickup(ItemHolder picker)
        {
            pickedUpBy = picker;
            gameObject.SetActive(false);
        }

        public void Drop()
        {
            pickedUpBy = null;
            gameObject.SetActive(true);
        }

        public void Drop(Vector3 pos)
        {
            transform.position = pos;
            gameObject.SetActive(true);
        }

        public void Use(GameObject user)
        {
            Debug.Log(name + "used by " + user.name);
            if (!isUsable)
                return;

            foreach (var effect in effectsWhenUsed)
            {
                //user.AddComponent(effect.GetType());
                var newEffect = objectPool.GetFreeInstance(effect.gameObject);
                newEffect.transform.SetParent(user.transform);
                newEffect.GetComponent<StatusEffect>().ParentIsSet();
            }

            usesLeft--;
            if (usesLeft <= 0 && destroyWhenUsed)
            {
                objectPool.Free(gameObject);
            }
        }

        public void StackWith(Item otherItem)
        {

            if (Equals(otherItem))
            {
                Debug.Log("Stacking item " + name + " with " + otherItem.name);
                otherItem.usesLeft += usesLeft;
                objectPool.Free(gameObject);
            }
        }

        public string GetFullDescription() {
            return name + "\n" + itemDescription;
        }

        private GameObject Split(int splitAmount)
        {

            //var newInstance = Instantiate( gameObject );
            var newInstance = objectPool.GetFreeInstance(gameObject);

            if (usesLeft > splitAmount)
            {
                newInstance.GetComponent<Item>().usesLeft = splitAmount;
                usesLeft -= splitAmount;
            }
            else
            {
                newInstance.GetComponent<Item>().usesLeft = usesLeft;
                //Destroy(gameObject);
                objectPool.Free(gameObject);
            }

            return newInstance;
        }
    }
}