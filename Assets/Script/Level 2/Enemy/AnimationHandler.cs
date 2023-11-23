using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator anim;
    public bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
}
