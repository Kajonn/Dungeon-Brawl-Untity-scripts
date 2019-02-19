using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl
{
    public class Killabel : MonoBehaviour
    {

        public ParticleSystem destroyedEffect;

        public void HealthDepleted()
        {

            if (destroyedEffect != null)
            {
                destroyedEffect.Play();
            }

            var items = GetComponent<ItemHolder>();
            if (items != null)
            {
                items.DropAllItems(transform.position);
            }

            var equipment = GetComponent<EquipmentHolder>();
            if (equipment != null)
            {
                equipment.DropAllEquipment();
            }

            Destroy(gameObject);
        }
    }
}