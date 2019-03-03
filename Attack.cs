using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    public class Attack : MonoBehaviour
    {

        public GameObject hitParticleSystemPrefab;

        private bool lastingDamage;
        private int damage;
        private float lifetimeSeconds;
        private GameObject hitParticleSystem;
        private List<SpriteRenderer> hitSpriteRenderer = new List<SpriteRenderer>();
        private string targetTag;
        private AttackEffectCallback callback;
        private float range;
        private float speed;
        private bool hit;
        private Vector3 direction;
        private float distanceTraveled = 0;
        private ObjectPool objectPool;

        public void SetProperties(
            bool lastingDamageIn,
            int damageIn,
            float lifeTimeSecondsIn,
            string targetTagIn,
            float rangeIn,
            float speedIn,
            AttackEffectCallback callbackIn)
        {
            lastingDamage = lastingDamageIn;
            damage = damageIn;
            lifetimeSeconds = lifeTimeSecondsIn;
            targetTag = targetTagIn;
            range = rangeIn;
            speed = speedIn;
            callback = callbackIn;
        }


        private void Update()
        {
            if (speed > 0 && !hit) {
                float distance = Time.deltaTime * speed;
                distanceTraveled += distance;
                transform.position += distance * direction;

                if (distanceTraveled > range) {
                    Deactivate();
                }
            }
        }

        // Use this for initialization
        void Start()
        {
            objectPool = FindObjectOfType<ObjectPool>();
        }

        public void StartAttack() {
            Debug.Log("Attack " + name + " created.");

            if (hitParticleSystemPrefab != null)
            {
                hitParticleSystem = Instantiate<GameObject>(hitParticleSystemPrefab, gameObject.transform);
                hitParticleSystem.SetActive(false);
            }
            if (range == 0)
            {
                StartDestroy();
            }
            hit = false;
            direction = transform.rotation * new Vector3(1, 0, 0);
            distanceTraveled = 0;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Collision(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            CollisionStay(collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Collision(collision.collider);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            CollisionStay(collision.collider);
        }

        private void Collision(Collider2D collision) {
            if (collision.tag.Equals(targetTag))
            {
                hitSpriteRenderer.Add(collision.GetComponent<SpriteRenderer>());
                collision.GetComponent<SpriteRenderer>().color = Color.red;
                collision.GetComponent<DamageTaker>().TakeDamage(damage);
                if (hitParticleSystem != null && hitParticleSystem.GetComponent<ParticleSystem>().isStopped)
                {
                    hitParticleSystem.GetComponent<ParticleSystem>().Play();
                }

                if (callback != null)
                {
                    callback.Hit(collision.gameObject, objectPool);
                }

                if (range > 0)
                {
                    gameObject.SetActive(false);
                    StartDestroy();
                }


                hit = true;
            }
            else if (collision.tag.Equals("Wall"))
            {
                Deactivate();
            }
        }

        private void CollisionStay(Collider2D collision)
        {
            if (lastingDamage && collision.tag.Equals(targetTag))
            {
                collision.GetComponent<DamageTaker>().TakeDamage(damage);
                if (hitParticleSystem)
                {
                    hitParticleSystem.GetComponent<ParticleSystem>().Play();
                }
            }
        }

        private void StartDestroy() {
            
            if (hitParticleSystem != null)
            {
                Destroy(hitParticleSystem, lifetimeSeconds);
            }
            Invoke("Deactivate", lifetimeSeconds);

        }

        private void Deactivate() {
            foreach (var spriteRenderer in hitSpriteRenderer)
            {
                if(spriteRenderer!=null)
                    spriteRenderer.color = Color.white;
            }
            hitSpriteRenderer.Clear();
            objectPool.Free(gameObject);
        }
    }
}