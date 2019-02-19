using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{

    [RequireComponent(typeof(EntityStats))]
    public class DamageDealer : MonoBehaviour
    {

        public string targetTag;

        private EntityStats entityStats;
        private float lastAttackDuration = 0;
        private float lastAttackStartTime = 0;
        private ObjectPool objectPool;

        private void Start()
        {
            entityStats = GetComponent<EntityStats>();
            objectPool = FindObjectOfType<ObjectPool>();
        }

        public void DoAttack(Attack attack, AttackProperties attackProperties, AttackEffectCallback callback, Vector2 position, Quaternion rotation)
        {
            Debug.Log(name + " trying to attack");

            if (attack != null && 
                (lastAttackStartTime == 0 || (Time.time - lastAttackStartTime > lastAttackDuration)) )
            {
                Debug.Log(name + " instantiating new attack");
                Attack ongoingAttack = objectPool.GetFreeInstance(attack.gameObject).GetComponent<Attack>();
                ongoingAttack.transform.position = position;
                ongoingAttack.transform.rotation = rotation;
                int damage = attackProperties.damage + (int)(Constants.StrengthToDamage * entityStats.GetStrength());
                float attackDuration = attackProperties.lifetimeSeconds * (1.0f - (Constants.SpeedToAttackSpeedMultiplier * entityStats.GetSpeed()));
                ongoingAttack.SetProperties(
                    attackProperties.lastingDamage, 
                    damage,
                    attackDuration, 
                    targetTag,
                    attackProperties.range,
                    attackProperties.speed,
                    callback);
               
                lastAttackStartTime = Time.time;
                lastAttackDuration = attackDuration;

                ongoingAttack.StartAttack();
            }
        }

    }
}