using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject spawnMob;
    void Awake()
    {
        foreach (var mob in FindObjectsOfType<Mob>())
        {
            var instance = Instantiate(spawnMob, mob.transform.position, mob.transform.rotation);
            instance.GetComponent<Spawner>().instance = mob.gameObject;
        }
    }
}
