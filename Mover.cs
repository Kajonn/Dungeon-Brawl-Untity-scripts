using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EntityStats))]
    public class Mover : MonoBehaviour
    {

        private bool canMove = true;

        private Vector2 velocity;
        private bool move = false;
        private Rigidbody2D rigidBody;
        private EntityStats entityStats;

        public bool CanMove { set => canMove = value; }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            entityStats = GetComponent<EntityStats>();
        }

        private void Update()
        {
            if (canMove)
            {
                if (move)
                {
                    rigidBody.velocity = velocity;
                    move = false;
                }
                else
                {
                    rigidBody.velocity = Vector2.zero;
                }
            }
            else {
                move = false;
            }
        }

        public void MoveInDirection(Vector3 direction)
        {
            float speedUnitPerSecond = entityStats.GetSpeed() * Constants.SpeedToMovementPerSecond;
            velocity = direction.normalized * speedUnitPerSecond;
            move = true;
        }
    }
}