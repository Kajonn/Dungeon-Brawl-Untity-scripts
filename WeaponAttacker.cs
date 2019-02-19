using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(EquipmentHolder))]
    [RequireComponent(typeof(DamageDealer))]
    public class WeaponAttacker : MonoBehaviour
    {

        private EquipmentHolder equipmentHolder;
        private DamageDealer damageDealer;
        private BoxCollider2D boxCollider2D;


        // Use this for initialization
        void Start()
        {
            equipmentHolder = GetComponent<EquipmentHolder>();
            damageDealer = GetComponent<DamageDealer>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void Attack(EquipmentSlot slot)
        {

            var equipment = equipmentHolder.GetEquipment(slot);
            if (equipment == null || equipment.GetType() != typeof(Weapon))
                return;

            Weapon weapon = (Weapon)equipment;
            Attack attack = weapon.attack;

            Assert.IsTrue(attack != null);

            var boundingBoxSize = boxCollider2D.size.x;
            var vec = new Vector3(boundingBoxSize, 0, 0);
            var worldPos = gameObject.transform.localToWorldMatrix.MultiplyPoint(vec);
            damageDealer.DoAttack(
                attack, 
                weapon.attackProperties, 
                weapon.attackEffectCallback,
                worldPos, 
                gameObject.transform.rotation);
        }
    }
}