using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
    public PlayerMovement pm;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!pm.getsInput && pm.isGrounded)
        {
            if (Input.GetKeyDown("1"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote1", true);
                Invoke("ResetEmotes", 3);
            }
            else if (Input.GetKeyDown("2"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote2", true);
                Invoke("ResetEmotes", 8);
            }
            else if (Input.GetKeyDown("3"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote3", true);
                Invoke("ResetEmotes", 2);
            }
            else if (Input.GetKeyDown("4"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote4", true);
                Invoke("ResetEmotes", 8);
            }
            else if (Input.GetKeyDown("5"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote5", true);
                Invoke("ResetEmotes", 10);
            }
            else if (Input.GetKeyDown("6"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote6", true);
                Invoke("ResetEmotes", 6.5f);
            }
            else if (Input.GetKeyDown("7"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote1", true);
                Invoke("ResetEmotes", 24.20f);
            }
            else if (Input.GetKeyDown("8"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote8", true);
                Invoke("ResetEmotes", 7);
            }
            else if (Input.GetKeyDown("9"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote9", true);
                Invoke("ResetEmotes", 3.20f);
            }
            else if (Input.GetKeyDown("0"))
            {
                ResetEmotes();
                anim.SetBool("doingEmote10", true);
                Invoke("ResetEmotes", 9);
            }
        }
        else
        {
            ResetEmotes();
        }
    }
    public void ResetEmotes()
    {
        anim.SetBool("doingEmote1", false);
        anim.SetBool("doingEmote2", false);
        anim.SetBool("doingEmote3", false);
        anim.SetBool("doingEmote4", false);
        anim.SetBool("doingEmote5", false);
        anim.SetBool("doingEmote6", false);
        anim.SetBool("doingEmote7", false);
        anim.SetBool("doingEmote8", false);
        anim.SetBool("doingEmote9", false);
        anim.SetBool("doingEmote10", false);
    }
}
