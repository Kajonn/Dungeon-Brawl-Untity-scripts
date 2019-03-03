using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ObjectDetector))]
    public class PlayerFollower : MonoBehaviour
    {

        public float IdleUpdateSpeedS = 8.0f;
        public float IdleWalkingDistance = 0.01f;

        private bool atPlayer = false;
        private Mover mover;
        private PlayerAttacker playerAttacker;
        private ObjectDetector objectDetector;

        private float lastIdleUpdate = 0;
        private Vector3 idleDir;

        private void Start()
        {
            mover = GetComponent<Mover>();
            playerAttacker = GetComponent<PlayerAttacker>();
            objectDetector = GetComponent<ObjectDetector>();
        }

        // Update is called once per frame
        void Update()
        {
            if (objectDetector.TargetObject == null)
            {
                objectDetector.TargetObject = GameObject.FindWithTag("Player");
            }
            else if( objectDetector.ObjectDetected) {
                UpdatePlayerDetected();
            } else 
            {
                UpdateIdling();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == objectDetector.TargetObject)
            {
                atPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject == objectDetector.TargetObject)
            {
                atPlayer = false;
            }
        }

        private void UpdatePlayerDetected() {
            var towardPlayer = objectDetector.TargetObject.transform.position - transform.position;
            transform.rotation = Utils.GetRotationFromDirection(towardPlayer);

            if (!atPlayer &&
                !(playerAttacker != null &&
                  playerAttacker.InRange))
            {
                mover.MoveInDirection(towardPlayer);
            }            
        }

        private void UpdateIdling() {

            lastIdleUpdate -= Time.deltaTime;

            if (lastIdleUpdate <= 0)
            {
                Vector3 randomPos = Random.insideUnitCircle * IdleWalkingDistance;
                randomPos += transform.position;
                idleDir = randomPos - transform.position;

                transform.rotation = Utils.GetRotationFromDirection(idleDir);
                
                lastIdleUpdate = Random.Range(IdleUpdateSpeedS / 2, IdleUpdateSpeedS);
            }
            else {
                mover.MoveInDirection(idleDir);
            }

        }
    }
}