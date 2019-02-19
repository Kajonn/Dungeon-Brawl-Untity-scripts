using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl {

    public class RandomStartItems : MonoBehaviour
    {
        [System.Serializable]
        public struct DropItem {
            public Item  ItemAsset;
            public float Percentage;
        }

        public List<DropItem> dropItems; 

        // Start is called before the first frame update
        void Start()
        {
            ItemHolder itemHolder = GetComponent<ItemHolder>();
            ObjectPool objectPool = FindObjectOfType<ObjectPool>();
            foreach (var dropItem in dropItems ) {
                if (Random.Range(0, 100) < dropItem.Percentage) {
                    var item = objectPool.GetFreeInstance(dropItem.ItemAsset.gameObject).GetComponent<Item>();
                    itemHolder.Pickup(item);
                }
            }

        }
        
    }
}