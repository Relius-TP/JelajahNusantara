using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject[] healthICon;
    public int healthPoin = 3;
    public GameObject gameOverUI;
    public PlayerData playerData;
    public Button back;

    private void Start()
    {
        healthPoin = playerData.hero_health;
    }

    private void OnEnable()
    {
        QTEController.OnQTEResult += SetHealth;
        PotionDetection.GetLifePotion += GetLifePotion;
    }

    private void OnDisable()
    {
        QTEController.OnQTEResult -= SetHealth;
        PotionDetection.GetLifePotion -= GetLifePotion;
    }

    private void SetHealth(bool result)
    {
        if(!result && healthPoin >= 1)
        {
            healthPoin--;
        }
    }

    private void Update()
    {
        ResetHealthUI();
        for (int i = 0; i < healthPoin; i++)
        {
            healthICon[i].SetActive(true);
        }

        if (healthPoin == 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        back.onClick.AddListener(BackMain);
    }

    private void ResetHealthUI()
    {
        foreach (var item in healthICon)
        {
            item.SetActive(false);
        }
    }

    private void GetLifePotion(int i)
    {
        if(healthPoin != playerData.hero_health)
        {
            healthPoin += i;
        }
    }
    void BackMain()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
