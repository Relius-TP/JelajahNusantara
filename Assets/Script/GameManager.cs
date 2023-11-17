using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject QteUi;
    public GameObject QteSys;
    public GameObject SkillCheckUI;
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject JumpScareScene;

    public PlayerData playerData;
    public GameObject keyIcon;

    //public AudioClip clip;
    //private AudioSource playerAudioSource;

    private void Start()
    {
        QteSys.SetActive(false);
        QteUi.SetActive(false);
        SkillCheckUI.SetActive(false);
        //WinScreen.SetActive(false);
        //GameOverScreen.SetActive(false);
        //JumpScareScene.SetActive(false);
        //playerAudioSource = GameObject.FindFirstObjectByType<AudioSource>().GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SkillCheckController.isWaitingInput == false)
        {
            SkillCheckUI.SetActive(false);
        }

        keyIcon.SetActive(playerData.IsHoldingKey);
    }

    private void OnEnable()
    {
        PlayerMovement.OnCaught += GetCaught;
        Interactable.OnGetItem += GetQuestItem;
        Portal.OnSkillCheckRunning += OpeningPortal;
    }

    private void OnDisable()
    {
        PlayerMovement.OnCaught -= GetCaught;
        Interactable.OnGetItem -= GetQuestItem;
        Portal.OnSkillCheckRunning -= OpeningPortal;
    }

    private void GetCaught()
    {
        //StartCoroutine(JumpScare());
        QteUi.SetActive(true);
        QteSys.SetActive(true);
        Time.timeScale = 0f;
    }

    //IEnumerator JumpScare()
    //{
    //    JumpScareScene.SetActive(true);
    //    playerAudioSource.PlayOneShot(clip);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    JumpScareScene.SetActive(false);
    //}

    private void GetQuestItem()
    {
        SkillCheckUI.SetActive(true);
        SkillCheckController.isWaitingInput = true;
    }

    private void OpeningPortal()
    {
        SkillCheckUI.SetActive(true);
        SkillCheckController.isWaitingInput = true;
    }

    //public void RestartGame()
    //{
    //    UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
    //    SceneManager.LoadScene(scene.name);
    //}
}
