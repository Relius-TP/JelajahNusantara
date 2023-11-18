using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    public float maxSize = 250.0f;
    private float speed = 10.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mengambil posisi pemain
        Vector3 playerPosition = PlayerOnBosStage.playerPosition;

        // Mengatur posisi anchored bar di atas pemain dengan offset (5 unit ke atas dari pemain)
        bar.anchoredPosition = new Vector2(playerPosition.x, playerPosition.y + 5);

        // Memindahkan Health Bar bersama dengan karakter menggunakan Rigidbody2D
        rb.velocity = new Vector2(playerPosition.x * speed, playerPosition.y);

        // Membatasi posisi Health Bar agar tetap di atas karakter
        Vector3 clampedPosition = bar.anchoredPosition;
        clampedPosition.y = Mathf.Max(clampedPosition.y, playerPosition.y + 5);
        bar.anchoredPosition = clampedPosition;
    }

    public void SetSize(float sizeNormalized)
    {
        // Mengatur ukuran bar
        bar.localScale = new Vector3(sizeNormalized * maxSize, 1f, 1f);
    }
}
