using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDetection : MonoBehaviour
{
    public static event Action<int> GetLifePotion;
    public static event Action<float, float> GetSpeedPotion;
    public static event Action<float, float> GetVisionPotion;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LifePotion"))
        {
            GetLifePotion?.Invoke(1);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("SpeedPotion"))
        {
            GetSpeedPotion?.Invoke(2f, 5f);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("VisionPotion"))
        {
            GetVisionPotion?.Invoke(7f, 5f);
            other.gameObject.SetActive(false);
        }
    }
}
