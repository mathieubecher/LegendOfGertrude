using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFunction : MonoBehaviour
{
    public Sword sword;
    public Controller controller;
    public AudioSource source;

    public List<AudioClip> steps;
    public List<AudioClip> attack;
    public AudioClip dead;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        
    }
    
    public void ActivateWeapon()
    {
        sword.attach = true;
    }

    public void EndAttack()
    {
        controller.ResetAttack();
    }
    public void EndDamage()
    {
        controller.ResetDamage();
    }

    public void PlayStep()
    {
        source.PlayOneShot(steps[Random.Range(0, steps.Count)], 0.2f);
    }
    public void PlayAttack()
    {
        source.PlayOneShot(attack[Random.Range(0, attack.Count)]);
    }
    public void PlayDead()
    {
        source.PlayOneShot(dead);
    }

}
