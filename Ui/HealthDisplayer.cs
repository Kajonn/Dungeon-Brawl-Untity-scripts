using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace dungeonbrawl.UI
{
    public class HealthDisplayer : MonoBehaviour
    {

        public GameObject HealthPanel;

        private Slider healthBar;
        private Text healthText;
        private EntityStats entityStats;

        private void OnDestroy()
        {
            Update();
        }

        // Use this for initialization
        void Start()
        {
            healthBar = HealthPanel.GetComponentInChildren<Slider>();
            healthText = HealthPanel.GetComponentInChildren<Text>();
            entityStats = GetComponent<EntityStats>();
        }

        // Update is called once per frame
        void Update()
        {

            int baseHealth = (int)entityStats.GetBaseHealth();
            int currentHealth = (int)entityStats.GetHealth();

            healthText.text = currentHealth.ToString() + "/" + baseHealth.ToString();
            healthBar.value = (float)currentHealth / (float)baseHealth;

        }

    }
}