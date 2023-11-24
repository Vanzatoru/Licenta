using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public Transform playerCamera;
    public float cameraFollowSpeed = 5f;

    private void Update()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed;

        transform.Translate(moveVelocity * Time.deltaTime);

        // Player rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);

        // Camera follow player
        Vector3 targetPosition = transform.position + transform.forward * -5f + transform.up * 2f; // Adjust the position as needed

        playerCamera.position = Vector3.Lerp(playerCamera.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
        playerCamera.LookAt(transform.position + transform.up * 2f); // Adjust the look-at position as needed
    }
}
