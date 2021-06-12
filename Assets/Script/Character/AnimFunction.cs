using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunction : MonoBehaviour
{
    public Sword sword;

    public void ActivateWeapon()
    {
        sword.attach = true;
    }
}
