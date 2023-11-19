using UnityEngine;

public class DestroyQTEButton : MonoBehaviour
{
    private void OnEnable()
    {
        QTEController.OnQTEResult += DestroyQTEImg;
    }

    private void OnDestroy()
    {
        QTEController.OnQTEResult -= DestroyQTEImg;
    }

    private void DestroyQTEImg(bool result)
    {
        Destroy(gameObject);
    }
}
