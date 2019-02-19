using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl
{
    [RequireComponent(typeof(ItemHolder))]
    public class PickItemOnTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var item = collision.GetComponent<Item>();
            if (item != null)
            {
                GetComponent<ItemHolder>().Pickup(item);
            }
        }
    }
}