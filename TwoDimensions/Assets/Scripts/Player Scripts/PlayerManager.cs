using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerKey playerKey;

    // Start is called before the first frame update
    void Start()
    {
        playerKey = GetComponent<PlayerKey>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
