using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    public bool isHoldingKey = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Key"){
            isHoldingKey = true;
            Destroy(other.gameObject.transform.parent.gameObject);
        }

    }

    public bool getKeyState(){
        return isHoldingKey;
    }

    public void setKeyState(bool state){
        isHoldingKey = state;
    }
}
