using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimListenMob : MonoBehaviour
{
    [SerializeField] private Mob _mob;

    public void EndAttack()
    {
        if (_mob == null) return;
        _mob.fsm.SetBool("Attack",false);
    }
    public void EndJump()
    { 
        if (_mob == null) return;
        _mob.fsm.SetBool("Jump",false);
    }
}
