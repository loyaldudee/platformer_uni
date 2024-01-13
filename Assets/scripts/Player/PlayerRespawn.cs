using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip CheckpointSound;
    private Transform currentCheckpoint;
    private  Health playerHealth;
    // public GameObject Spawner;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = currentCheckpoint.position;
        
        playerHealth.Respawn();

        //Camera.main.GetComponent<CameraController>()MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player")
        {
            currentCheckpoint = other.transform;
            SoundManager.instance.PlaySound(CheckpointSound);
            gameObject.GetComponent<Animator>().SetTrigger("appear");
            // other.GetComponent<Collider>().enabled = false;
        }
    }
    
}
