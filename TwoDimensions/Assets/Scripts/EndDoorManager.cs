using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoorManager : MonoBehaviour
{
    public GameManagement gameManagement;
    public List<GameObject> locks;
    public GameObject unlockedLock;

    // False: Locked
    private List<bool> lockStates;

    // Start is called before the first frame update
    void Start()
    {
        lockStates = new List<bool>(){false, false, false};
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            if (other.gameObject.GetComponent<PlayerKey>().getKeyState()){
                other.gameObject.GetComponent<PlayerKey>().setKeyState(false);
                unlockLock();
            }
        }
    }

    private void unlockLock(){
        for (int i = 0; i < lockStates.Count; i ++){
            if (locks[i] == null)
                continue;

            if (lockStates[i] == false){
                Vector3 pos = locks[i].transform.position;
                Transform parent = locks[i].transform.parent;

                Destroy(locks[i]);
                Instantiate(unlockedLock, pos, Quaternion.identity, parent);

                gameManagement.doorUnlocked();
                break;
            } 
        }
    }
}
