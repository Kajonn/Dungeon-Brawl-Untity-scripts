using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{

    [RequireComponent(typeof(ItemHolder))]
    public class EquipmentHolder : MonoBehaviour
    {

        public List<Equipment> equipmentList;

        private Dictionary<EquipmentSlot, Equipment> EquipmentSlots = new Dictionary<EquipmentSlot, Equipment>();
        private Dictionary<Equipment, EquipmentSlot> SlotsToEquipment = new Dictionary<Equipment, EquipmentSlot>();


        private void Start()
        {
            ObjectPool objectPool = FindObjectOfType<ObjectPool>();
            List<Equipment> equipmentsToAdd = new List<Equipment>();
            List<Equipment> equipmentsToRemove = new List<Equipment>();
            foreach (Equipment equipment in equipmentList)
            {
                if (Common.Utils.IsPrefab(equipment.gameObject))
                {
                    equipmentsToAdd.Add(objectPool.GetFreeInstance(equipment.gameObject).GetComponent<Equipment>());
                    equipmentsToRemove.Add(equipment);
                }
            }
            foreach (Equipment equipment in equipmentsToRemove)
            {
                equipmentList.Remove(equipment);
            }
            foreach (Equipment equipment in equipmentsToAdd)
            {
                equipmentList.Add(equipment);
            }
            foreach (var equipment in equipmentList)
            {
                Equip(equipment, equipment.equipmentSlot);
            }
        }

        public Equipment GetEquipment(EquipmentSlot slot)
        {
            if (EquipmentSlots.ContainsKey(slot))
                return EquipmentSlots[slot];
            else
                return null;
        }

        private void ItemDestroyed(Item item)
        {
            Equipment toRemove = null;
            foreach (var equipment in equipmentList)
            {
                if (equipment.GetComponent<Item>() == item)
                {
                    toRemove = equipment;
                    break;
                }
            }

            if (toRemove != null)
            {
                Debug.Log("Removing destroyed item from equipment: " + item.name);

                equipmentList.Remove(toRemove);
                var slot = SlotsToEquipment[toRemove];
                SlotsToEquipment.Remove(toRemove);
                EquipmentSlots.Remove(slot);
            }
        }

        public void Equip(Equipment equipment, EquipmentSlot slot)
        {
            UnEquip(slot);

            var itemHolder = GetComponent<ItemHolder>();
            if (itemHolder != null)
            {
                var item = equipment.GetComponent<Item>();
                if (item)
                {
                    itemHolder.Drop(item, new Vector3());
                    item.Pickup(GetComponent<ItemHolder>());
                }
            }

            if (!equipmentList.Contains(equipment))
            {
                equipmentList.Add(equipment);
            }

            EquipmentSlots.Add(slot, equipment);
            SlotsToEquipment.Add(equipment, slot);

            Debug.Log("Equipment " + equipment.name + " equiped");

            SendMessage("EquipmentUpdated", SendMessageOptions.DontRequireReceiver);
        }


        public void UnEquip(EquipmentSlot slot)
        {
            Debug.Log("Unequipping " + slot.ToString());

            Equipment equipment = null;
            if (EquipmentSlots.ContainsKey(slot))
            {
                equipment = EquipmentSlots[slot];
                Debug.Log("Unequipping " + equipment.name);

                EquipmentSlots.Remove(slot);
                equipmentList.Remove(equipment);
                SlotsToEquipment.Remove(equipment);

                var itemHolder = gameObject.GetComponent<ItemHolder>();
                if (equipment != null && itemHolder != null)
                {
                    var item = equipment.GetComponent<Item>();
                    itemHolder.Pickup(item);
                }

                SendMessage("EquipmentUpdated", SendMessageOptions.DontRequireReceiver);
            }
        }

        public void DropAllEquipment()
        {
            //Copy list as Drop will modify 
            List<Equipment> equipmentToDrop = new List<Equipment>(equipmentList);
            foreach (var equipment in equipmentToDrop)
            {
                equipment.GetComponent<Item>().Drop(transform.position);
            }
        }
    }
}