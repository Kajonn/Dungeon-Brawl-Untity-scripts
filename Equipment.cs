using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{

    [RequireComponent(typeof(EntityStats))]
    public class Equipment : MonoBehaviour
    {

        public EquipmentSlot equipmentSlot;

        public float strengthModifier;
        public float speedModifier;
        public float healthModifier;
        public float defenseModifier;

        public List<StatusEffect> effectsWhenEquipped;

        public List<StatusEffect> activeEffects;

        private ObjectPool objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();
        }

        public void EquipedBy(GameObject user)
        {
            Debug.Log(user.name + " equipped " + name);
            foreach (var effect in effectsWhenEquipped)
            {
                var newEffect = objectPool.GetFreeInstance(effect.gameObject).GetComponent<StatusEffect>();
                newEffect.transform.SetParent(user.transform);
                activeEffects.Add(newEffect);
            }

            var entityStats = user.GetComponent<EntityStats>();
            entityStats.ChangeBaseDefense(strengthModifier);
            entityStats.ChangeBaseSpeed(speedModifier);
            entityStats.ChangeBaseHealth(healthModifier);
            entityStats.ChangeBaseDefense(defenseModifier);
        }

        public void UnEquipedBy(GameObject user)
        {
            Debug.Log(user.name + " unequipped " + name);
            foreach (var effect in activeEffects)
            {
                if (effect != null)
                {
                    objectPool.Free(effect.gameObject);
                }
            }

            var entityStats = user.GetComponent<EntityStats>();
            entityStats.ChangeBaseDefense(-strengthModifier);
            entityStats.ChangeBaseSpeed(-speedModifier);
            entityStats.ChangeBaseHealth(-healthModifier);
            entityStats.ChangeBaseDefense(-defenseModifier);
        }


    }
}