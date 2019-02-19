using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(Mover))]
    public class PlayerFollower : MonoBehaviour
    {

        private GameObject player;
        private bool atPlayer = false;
        private Mover mover;
        private PlayerAttacker playerAttacker;

        private void Start()
        {
            mover = GetComponent<Mover>();
            playerAttacker = GetComponent<PlayerAttacker>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            else
            {

                var towardPlayer = player.transform.position - transform.position;
                transform.rotation = Utils.GetRotationFromDirection(towardPlayer);

                if (!atPlayer &&
                    !(playerAttacker!=null &&
                      playerAttacker.InRange) )
                {
                    mover.MoveInDirection(towardPlayer);
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == player)
            {
                atPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject == player)
            {
                atPlayer = false;
            }
        }
    }
}