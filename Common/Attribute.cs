using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dungeonbrawl.Common
{

    public class Attribute
    {
        
        private float baseStatus;
        private float currentStatus;
        
        public Attribute(float initalValue)
        {
            baseStatus = initalValue;
            currentStatus = initalValue;
        }
        

        public float BaseStatus
        {
            get
            {
                return baseStatus;
            }

            set
            {
                baseStatus = value;
                if (CurrentStatus < baseStatus) {
                    CurrentStatus = baseStatus;
                }
            }
        }

        public float CurrentStatus
        {
            get
            {
                return currentStatus;
            }

            set
            {
                currentStatus = (value >= 0 ? value : 0);
            }
        }

        public void ResetStatus() {
            CurrentStatus = baseStatus;
        }
    }
}
