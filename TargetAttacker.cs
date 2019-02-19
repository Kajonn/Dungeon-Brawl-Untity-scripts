using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(DamageDealer))]
    public class TargetAttacker : MonoBehaviour
    {

        public Attack attack;
        public AttackProperties attackProperties;
        public AttackEffectCallback attackEffectCallback;

        private DamageDealer damageDealer;

        private void Start()
        {
            damageDealer = GetComponent<DamageDealer>();
        }

        public void Attack(Vector3 target)
        {
            damageDealer.DoAttack(
                attack, 
                attackProperties, 
                attackEffectCallback, 
                target, 
                gameObject.transform.rotation);

        }
    }
}