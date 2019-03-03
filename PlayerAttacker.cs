using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(ObjectDetector))]
    public class PlayerAttacker : MonoBehaviour
    {

        private bool inRange;
        
        private bool atPlayer = false;
        private WeaponAttacker weaponAttacker;
        private TargetAttacker targetAttacker;
        private UnarmedAttacker unarmedAttacker;
        private EquipmentHolder equipmentHolder;
        private ObjectDetector objectDetector;

        public bool InRange { get => inRange; }
        
        private void Start()
        {
            weaponAttacker = GetComponent<WeaponAttacker>();
            targetAttacker = GetComponent<TargetAttacker>();
            unarmedAttacker = GetComponent<UnarmedAttacker>();
            equipmentHolder = GetComponent<EquipmentHolder>();
            objectDetector = GetComponent<ObjectDetector>();
        }

        private void Update()
        {
            if (objectDetector.TargetObject == null)
            {
                objectDetector.TargetObject = GameObject.FindWithTag("Player");
            }
            else if(objectDetector.ObjectDetected) {
                TryAttack(objectDetector.TargetObject);
            }

        }

        public void TryAttack(GameObject player)
        {

            float distance = (player.transform.position - transform.position).magnitude;
            
            if (weaponAttacker != null)
            {
                var equipment = equipmentHolder.GetEquipment(EquipmentSlot.RightHand);
                if (equipment != null && 
                    equipment.GetType() != typeof(Weapon))
                {
                    Weapon weapon = (Weapon)equipment;
                    if (atPlayer || weapon.attackProperties.range > distance)
                    {
                        weaponAttacker.Attack(EquipmentSlot.RightHand);
                        inRange = true;
                        return;
                    }
                }
            }

            if (targetAttacker != null &&
                (atPlayer ||
                targetAttacker.attackProperties.range > distance))
            {
                targetAttacker.Attack(player.transform.position);
                inRange = true;
                return;
            }

            if (unarmedAttacker != null &&
                (atPlayer ||
                unarmedAttacker.attackProperties.range > distance))
            {
                unarmedAttacker.Attack();
                inRange = true;
                return;
            }

            inRange = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == objectDetector.TargetObject)
            {
                atPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject == objectDetector.TargetObject)
            {
                atPlayer = false;
            }
        }

    }
}