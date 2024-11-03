using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using soundftx;

namespace player_control
{

    [RequireComponent(typeof(CharacterController))]
    public class Player_ : MonoBehaviour
{
        [SerializeField]
        CharacterController controller;

        public float speed = 3.0F;
        public float rotateSpeed = 3.0F;

        void Update()
        {
            controller = GetComponent<CharacterController>();

            // Rotate around y - axis
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * Input.GetAxis("Vertical");
            controller.SimpleMove(forward * curSpeed);
        }



    }


}