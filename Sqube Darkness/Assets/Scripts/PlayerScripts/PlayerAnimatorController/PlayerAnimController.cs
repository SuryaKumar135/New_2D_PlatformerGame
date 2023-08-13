using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator PlayerAnim;
    
    private void Awake()
    {
        PlayerAnim=GetComponent<Animator>();   
    }
    public void PlayerIdle(bool idle)
    {
       PlayerAnim.SetBool("Idle",idle);
    }
    public void PlayerRun(bool run)
    {
        PlayerAnim.SetBool("Run", run);
    }
    public void PlayerJump(int jump)
    {
        PlayerAnim.SetInteger("Jump", jump);

    }
    public void PlayerSlide(bool slide)
    {
        PlayerAnim.SetBool("Sliding", slide);

    }
    public void PlayerFall(int fall)
    {
        PlayerAnim.SetInteger("Falling",fall);

    }
}//class
