using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] public float playerMovementSpeed;
    [SerializeField] public Vector2 movementInput;
    [SerializeField] public bool playerLock = false;
    float minX, maxX;
    float minY, maxY;

    private void Start()
    {
        FixPlayerMovement();
    }

    private void FixPlayerMovement()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    private void Update()
    {
        if (!playerLock)
        {
            movementInput.x = Input.GetAxis("Horizontal") * Time.deltaTime * playerMovementSpeed;
            movementInput.y = Input.GetAxis("Vertical") * Time.deltaTime * playerMovementSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + movementInput.x, minX + 0.5f, maxX - 0.5f);
            var newYPos = Mathf.Clamp(transform.position.y + movementInput.y, minY + 0.5f, maxY - 0.5f);
            transform.position = new Vector2(newXPos, newYPos);
        }
    }
}
