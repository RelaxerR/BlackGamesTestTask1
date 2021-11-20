using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick PlayerJoystick; // Control joystick
    [SerializeField] private float PlayerSpeed; // Speed modifer
    [SerializeField] private GameObject MovePoint; // Tap position 3D
    private Rigidbody rb; // Player Rigit Body

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        // if joystick movement
        if (PlayerJoystick.Horizontal != 0 || PlayerJoystick.Vertical != 0){
            JoystickMove();
        }
        // if taps movement
        else{
            TapMove();
        }
    }

    private void JoystickMove(){
        MovePoint.transform.position = transform.position; // Deactivate last tap movement
        rb.velocity = new Vector3(PlayerJoystick.Direction.x * PlayerSpeed, rb.velocity.y, PlayerJoystick.Direction.y * PlayerSpeed);
    }
    private void TapMove(){
        var mpos = MovePoint.transform.position; // move point pos
        var ppos = transform.position; // player pos
        float xMove = 0; // Vector component X
        float zMove = 0; // Vector component Z

        // NOT A BUG, BUT A FEATURE
        if (mpos.x > ppos.x){
            xMove = PlayerSpeed;
        }
        if (mpos.x < ppos.x){
            xMove = -PlayerSpeed;
        }
        if (mpos.z > ppos.z){
            zMove = PlayerSpeed;
        }
        if (mpos.z < ppos.z){
            zMove = -PlayerSpeed;
        }

        rb.velocity = new Vector3(xMove, rb.velocity.y, zMove);
    }
}