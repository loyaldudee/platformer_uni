// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class door : MonoBehaviour
// {
//     [SerializeField] private Transform previousRoom;
//     [SerializeField] private Transform nextroom;
//     [SerializeField] private CameraController cam;
//     // Start is called before the first frame update

//     private void OnTriggerEnter2D(Collider2D collision) {
//         if (collision.tag == "Player")
//         {
//             if(collision.transform.position.x < transform.position.x)
//             {
//                 cam.MoveToNewRoom(nextroom);
//             }
//             else
//             {
//                 cam.MoveToNewRoom(previousRoom);
//             }
//         }
//     }
//     // void Start()
//     // {
        
//     // }

//     // // Update is called once per frame
//     // void Update()
//     // {
        
//     // }
// }
