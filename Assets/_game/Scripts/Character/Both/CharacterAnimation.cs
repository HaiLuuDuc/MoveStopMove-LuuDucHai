using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public string currentAnim;
    private void Start()
    {
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
