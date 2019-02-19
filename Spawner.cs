using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dungeonbrawl
{
    public class Spawner : MonoBehaviour
    {

        public float spawningRadius;
        public GameObject spawningObject;
        public float spawnTimeS;
        public int maxSpawnedObjects;

        private float sinceLastSpawnS = 0;
        private ObjectPool objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();    
        }

        // Update is called once per frame
        void Update()
        {
            sinceLastSpawnS += Time.deltaTime;
            if (sinceLastSpawnS > spawnTimeS)
            {

                if (spawningObject)
                {
                    var newSpawningPos = (Random.insideUnitCircle * spawningRadius) + new Vector2(transform.position.x, transform.position.y);
                    var spawnedObject = objectPool.GetFreeInstance(spawningObject);
                    spawnedObject.transform.position = newSpawningPos;
                }

                sinceLastSpawnS = 0;
            }
        }
    }
}