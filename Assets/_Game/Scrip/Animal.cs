using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] Animator anim;
    protected string animName = "idle";
    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsState(GameState.Playing))
        {
            ChangeAnim("play");
        }
    }
    protected void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }
}
