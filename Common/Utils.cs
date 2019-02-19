using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace dungeonbrawl.Common
{
    public class Utils
    {
        static public Quaternion GetRotationFromDirection(Vector2 dir)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        static public bool IsPrefab(GameObject gameObject)
        {
            return gameObject.scene.rootCount == 0;
        }
    }
        
}
