using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    public class HealingStatusEffect : StatusEffect
    {
        public float healthPerSecond = 0;

        // Update is called once per frame
        protected override void Update()
        {
            float amountToHeal = (healthPerSecond * Time.deltaTime);

            if ((entityStats.GetHealth() + amountToHeal) < entityStats.GetBaseHealth())
            {
                entityStats.ChangeHealth(amountToHeal);
            }
            else
            {
                entityStats.ChangeHealth(entityStats.GetBaseHealth() - entityStats.GetHealth());
            }

            base.Update();
        }
    }
}