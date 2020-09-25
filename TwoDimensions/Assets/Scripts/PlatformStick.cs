using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.transform.parent.SetParent(transform);
        }

        if (other.gameObject.tag == "Cube"){
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.transform.parent.SetParent(null);
        }

        if (other.gameObject.tag == "Cube"){
            other.gameObject.transform.SetParent(null);
        }
    }
}
