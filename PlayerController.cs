using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(WeaponAttacker))]
    public class PlayerController : MonoBehaviour
    {

        private Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {

            var xInput = Input.GetAxis("Horizontal");
            var yInput = Input.GetAxis("Vertical");

            var dir = new Vector3(xInput, yInput).normalized;
            if (dir.magnitude > 0)
            {
                mover.MoveInDirection(dir);
            }

            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            var rotateDir = (point - transform.position).normalized;
            transform.rotation = Utils.GetRotationFromDirection(rotateDir);

            if (Input.GetButtonDown("Fire1"))
            {
                GetComponent<WeaponAttacker>().Attack(EquipmentSlot.RightHand);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                GetComponent<WeaponAttacker>().Attack(EquipmentSlot.LeftHand);
            }

        }

    }
}