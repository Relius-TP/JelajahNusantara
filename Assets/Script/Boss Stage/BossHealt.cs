using UnityEngine;

public class BossHealt : MonoBehaviour
{
    public RectTransform bar;
    public float maxSize = 200.0f;

    public void SetSize(float sizeNormalized)
    {
        // Mengatur ukuran bar
        bar.localScale = new Vector3(sizeNormalized * maxSize, 1f, 1f);
    }
}
