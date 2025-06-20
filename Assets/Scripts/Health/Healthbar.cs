using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image HealthHeartTotal;
    [SerializeField] private Image HealthHeartCurrent;

    private void start()
    {
        HealthHeartTotal.fillAmount = playerHealth.currentHealth / 8;
    }

    private void Update()
    {
        HealthHeartCurrent.fillAmount = playerHealth.currentHealth / 8;
    }
}
