using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    public class EntityStats : MonoBehaviour
    {

        public float initialHealth;
        public float initialSpeed;
        public float initialStrength;
        public float initialDefense;

        private Attribute strength;
        private Attribute speed;
        private Attribute health;
        private Attribute defense;

        private void Start()
        {
            health = new Attribute(initialHealth);
            speed = new Attribute(initialSpeed);
            strength = new Attribute(initialStrength);
            defense = new Attribute(initialDefense);
        }
        
        public float GetHealth()
        {
            return health.CurrentStatus;
        }

        public float GetBaseHealth()
        {
            return health.BaseStatus;
        }

        public void ChangeHealth(float change)
        {
            health.CurrentStatus += change;
            if (health.CurrentStatus <= 0)
            {
                GetComponent<Killabel>().HealthDepleted();
            }
        }

        public void ChangeBaseHealth(float change)
        {
            health.BaseStatus += change;
            if (health.CurrentStatus <= 0)
            {
                GetComponent<Killabel>().HealthDepleted();
            }
        }

        public float GetStrength()
        {
            return strength.CurrentStatus;
        }

        public float GetBaseStrength()
        {
            return strength.BaseStatus;
        }

        public void ChangeStrength(float change)
        {
            strength.CurrentStatus += change;
        }

        public void ChangeBaseStrenght(float change)
        {
            strength.BaseStatus += change;
        }

        public float GetSpeed()
        {
            return speed.CurrentStatus;
        }

        public float GetBaseSpeed()
        {
            return speed.BaseStatus;
        }

        public void ChangeSpeed(float change)
        {
            speed.CurrentStatus += change;
        }

        public void ChangeBaseSpeed(float change)
        {
            speed.BaseStatus += change;
        }

        public float GetDefense()
        {
            return defense.CurrentStatus;
        }

        public float GetBaseDefense()
        {
            return defense.BaseStatus;
        }

        public void ChangeDefense(float change)
        {
            defense.CurrentStatus += change;
        }

        public void ChangeBaseDefense(float change)
        {
            defense.BaseStatus += change;
        }

    }
}