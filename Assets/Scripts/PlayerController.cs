using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick PlayerJoystick;
    [SerializeField] private float PlayerSpeed;
    [SerializeField] private Transform _tapMoveTransform;

    private Camera _camera;
    private Rigidbody _myRigitBody;

    private void Awake(){
        _camera = Camera.main;
    }
    private void Start()
    {
        _tapMoveTransform.position = transform.position;
        _myRigitBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate(){
        // if joystick movement
        if ((PlayerJoystick.Horizontal != 0) || (PlayerJoystick.Vertical != 0))
            JoystickMove();
        // if taps movement
        else{
            SetMovePos();
            TapMove();
        }
    }

    private void JoystickMove(){
        _tapMoveTransform.position = transform.position; // Deactivate last tap movement
        
        _myRigitBody.velocity = new Vector3(
            PlayerJoystick.Direction.x * PlayerSpeed,
            _myRigitBody.velocity.y,
            PlayerJoystick.Direction.y * PlayerSpeed
        );
    }
    private void TapMove(){
        var playerPos = transform.position;
        if (CheckApproximatePos(playerPos)) return;

        float xMove = 0; // Vector component X
        float zMove = 0; // Vector component Z

        // NOT A BUG, BUT A FEATURE
        if (_tapMoveTransform.position.x > playerPos.x){
            xMove = PlayerSpeed;
        }
        if (_tapMoveTransform.position.x < playerPos.x){
            xMove = -PlayerSpeed;
        }
        if (_tapMoveTransform.position.z > playerPos.z){
            zMove = PlayerSpeed;
        }
        if (_tapMoveTransform.position.z < playerPos.z){
            zMove = -PlayerSpeed;
        }

        _myRigitBody.velocity = new Vector3(xMove, _myRigitBody.velocity.y, zMove);
    }
    private bool CheckApproximatePos(Vector3 playerPos){
        var playerX = (int) playerPos.x;
        var playerZ = (int) playerPos.z;
        var targetX = (int) _tapMoveTransform.position.x;
        var targetZ = (int) _tapMoveTransform.position.z;

        return ((playerX == targetX) && (playerZ == targetZ));
    }
    private void SetMovePos(){
        if (Input.touches.Length == 0) return;

        var touch = Input.GetTouch(Input.touches.Length - 1);

        RaycastHit hit;
        if (Physics.Raycast (_camera.ScreenPointToRay(Input.mousePosition), out hit)) {
            _tapMoveTransform.position = hit.point;
        }
    }
}