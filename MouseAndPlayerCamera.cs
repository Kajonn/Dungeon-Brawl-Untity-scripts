using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndPlayerCamera : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        if (player != null)
        {
            var playerPos = player.transform.position;

            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}
