using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDestroy : MonoBehaviour
{
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == 8){
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity, null);
            Destroy(effect, 3f);
            Destroy(gameObject);
        }
    }
}
