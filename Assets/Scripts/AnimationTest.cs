using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        print(anim);
    }
    
    void Update()
    {
        bool q = Input.GetKey(KeyCode.Q);
        bool w = Input.GetKey(KeyCode.W);

        // ---- Q = WALK ----
        if (q && !w)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Run", false);
            anim.SetBool("Walking", true);
        }
        // ---- W = RUN ----
        else if (w)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Run", true);
        }
        // ---- IDLE ----
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
        }

        // ---- E = ATTACK ----
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Attack");
        }
    }
}

