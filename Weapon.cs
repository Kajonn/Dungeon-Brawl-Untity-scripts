using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dungeonbrawl.Common;

namespace dungeonbrawl
{
    public class Weapon : Equipment
    {

        public Attack attack;
        public AttackProperties attackProperties;

        public AttackEffectCallback attackEffectCallback;

    }
}