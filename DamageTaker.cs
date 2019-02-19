using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl
{

    [RequireComponent(typeof(EntityStats))]
    public class DamageTaker : MonoBehaviour
    {

        private EntityStats entityStats;

        // Use this for initialization
        void Start()
        {
            entityStats = gameObject.GetComponent<EntityStats>();
        }

        public void TakeDamage(int damage)
        {
            float defense = entityStats.GetDefense();
            float reducedDamage = Mathf.Max(damage - defense, 1);
            Debug.Log("TakeDamage " + damage + " reduced to " + reducedDamage + " by defense " + defense);
            entityStats.ChangeHealth(-reducedDamage);
        }

    }
}