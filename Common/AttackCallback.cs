using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace dungeonbrawl.Common
{
    [System.Serializable]
    public class AttackEffectCallback
    {
        public List<StatusEffect> effectsWhenAttacking;
        
        public void Hit(GameObject gameObjectHit, ObjectPool objectPool)
        {
            foreach (var effect in effectsWhenAttacking)
            {
                var newEffect = objectPool.GetFreeInstance(effect.gameObject);
                newEffect.transform.SetParent(gameObjectHit.transform);
                newEffect.GetComponent<StatusEffect>().ParentIsSet();
            }
        }
    }
}
