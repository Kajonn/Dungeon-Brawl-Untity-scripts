using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace dungeonbrawl.Common
{
    public class StatusEffect : MonoBehaviour
    {

        public float secondsActiveLeft = 0;
        public bool useActiveTimeLeft = false;

        protected EntityStats entityStats;
        private ObjectPool objectPool;
        private float currentSecondsActiveLeft;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();
            currentSecondsActiveLeft = secondsActiveLeft; 
        }

        public void ParentIsSet()
        {
            entityStats = GetComponentInParent<EntityStats>();
            Assert.IsTrue(entityStats != null);
            Activate();
        }

        protected virtual void Activate()
        {
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (useActiveTimeLeft)
            {
                currentSecondsActiveLeft -= Time.deltaTime;
                if (currentSecondsActiveLeft < 0)
                {
                    Deactivate();
                    objectPool.Free(gameObject);
                }
            }
        }

        protected virtual void Deactivate() {
        }


    }
}