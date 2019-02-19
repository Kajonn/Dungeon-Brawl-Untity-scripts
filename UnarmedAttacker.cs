using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(DamageDealer))]
    public class UnarmedAttacker : MonoBehaviour
    {

        public Attack attack;
        public AttackProperties attackProperties;
        public AttackEffectCallback attackEffectCallback;

        public void Attack()
        {
            var boundingBoxSize = gameObject.GetComponent<BoxCollider2D>().size.x;
            var vec = new Vector3(boundingBoxSize, 0, 0);
            var worldPos = gameObject.transform.localToWorldMatrix.MultiplyPoint(vec);

            GetComponent<DamageDealer>().DoAttack(
                attack, 
                attackProperties, 
                attackEffectCallback, 
                worldPos, 
                gameObject.transform.rotation);
        }
        
    }
}