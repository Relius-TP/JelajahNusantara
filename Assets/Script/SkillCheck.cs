using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheck : MonoBehaviour
{
    public Image finishPoint;
    public GameObject A;
    public GameObject B;
    private float aValue;
    private float bValue;

    public float randomA = 470;
    public float randomB = 551;
    private float randomPosition;

    public float speed = 5.0f;
    private bool isMoving = true;

    public RectTransform background;

    void Start()
    {
        randomPosition = Random.Range(randomA, randomB);
        finishPoint.transform.position = new Vector2(randomPosition, 308);

        aValue = A.gameObject.transform.position.x;
        bValue = B.gameObject.transform.position.x;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x >= background.position.x + background.rect.width / 2)
            {
                Debug.Log("Gagal");
                isMoving = false;
            }
        }

        else
        {
            if (transform.position.x >= bValue || transform.position.x <= aValue)
            {
                Debug.Log("Gagal");
            }
            else
            {
                Debug.Log("Berhasil");
            }

            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = false;
        }
    }
}
