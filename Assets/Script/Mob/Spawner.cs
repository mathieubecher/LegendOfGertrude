using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _mob;
    [SerializeField] private float _respawnTimer = 20f;
    private float _timer;
    private GameObject _instance;
    
    // Start is called before the first frame update
    void Start()
    {
        _instance = Instantiate(_mob, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (_instance == null)
        {
            _timer += Time.deltaTime;
            if (_timer > _respawnTimer)
            {
                _instance = Instantiate(_mob, transform.position, transform.rotation);
            }
        }
    }
}
