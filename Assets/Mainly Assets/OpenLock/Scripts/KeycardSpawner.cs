using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardSpawner : MonoBehaviour
{
    public GameObject keycardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnKeycard()
    {
        Instantiate(keycardPrefab, transform.position, transform.rotation);
    }
}
