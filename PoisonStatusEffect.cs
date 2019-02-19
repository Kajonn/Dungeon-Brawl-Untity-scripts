using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    public class PoisonStatusEffect : StatusEffect
    {
        public float damagePerSecond = 0;

        // Update is called once per frame
        protected override void Update()
        {
            entityStats.ChangeHealth(-damagePerSecond * Time.deltaTime);

            GetComponentInParent<SpriteRenderer>().color = Color.green;
            base.Update();
        }

        protected override void Deactivate()
        {
            GetComponentInParent<SpriteRenderer>().color = Color.white;
        }
    }
}