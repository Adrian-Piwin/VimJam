﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassManager : MonoBehaviour
{
    public GameManagement gameManagement;

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            gameManagement.playerPassedDoor();
        }
    }
}
