using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    public float maxSize = 100f;

    public void SetSize(float sizeNormalized)
    {
        // Mengatur ukuran bar
        bar.localScale = new Vector3(sizeNormalized * maxSize, 1f, 1f);
    }
}
