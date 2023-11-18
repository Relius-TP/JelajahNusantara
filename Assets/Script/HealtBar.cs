using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBar : MonoBehaviour
{
    public Transform bar;
    public float maxSize = 250.0f;

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized * maxSize, 1f, 1f);
    }
}
