using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _mob;
    [SerializeField] private float _respawnTimer = 20f;
    private float _timer;
    public GameObject instance;
    
    // Start is called before the first frame update
    void Start()
    {
        //_instance = Instantiate(_mob, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (instance == null)
        {
            _timer += Time.deltaTime;
            if (_timer > _respawnTimer)
            {
                instance = Instantiate(_mob, transform.position, transform.rotation);
                _timer = 0;
            }
        }
    }
}
