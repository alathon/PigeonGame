using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {
    [SerializeField]
    private MovementController movementController;

    private bool leftPressed = false;
    private bool rightPressed = false;
    private bool upPressed = false;
    private bool downPressed = false;

    void FixedUpdate()
    {
        Vector2 movement = new Vector2();
        if (leftPressed) movement += new Vector2(-1, 0);
        if (rightPressed) movement += new Vector2(1, 0);
        if (upPressed) movement += new Vector2(0, 1);
        if (downPressed) movement += new Vector2(0, -1);

        if (movement != Vector2.zero) movementController.Move(movement);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) upPressed = true;
        else upPressed = false;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) downPressed = true;
        else downPressed = false;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) leftPressed = true;
        else leftPressed = false;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) rightPressed = true;
        else rightPressed = false;
    }
}
