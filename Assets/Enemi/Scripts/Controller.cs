using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour
{

    public int health=100;
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    
    Vector2 rotation = Vector2.zero;
    private Vector3 basePositions;
    private bool isDead=false;


    


    //TIR
    public int speedNormal;
    public int speedDecors;
    public int speedSuper;
    private int lastshoot=0;
    public Transform pfBulletNormalProjectile;
    public Transform pfBulletDecorsProjectile;
    public Transform pfBulletSuperProjectile;

    public Transform spawnBulletPosition;
    private Vector3 mouseWorldPosition;


    public LayerMask aimColliderLayerMask= new LayerMask();

    [HideInInspector]
    public bool canMove = true;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {

        if(!isDead)
        {
            if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);

            mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint= new Vector2(Screen.width/2F,Screen.height/2f);
            Ray ray=Camera.main.ScreenPointToRay(screenCenterPoint);
            if(Physics.Raycast(ray,out RaycastHit raycasthHit, 999f, aimColliderLayerMask)){
                //transform.position=raycasthHit.point;
                mouseWorldPosition=raycasthHit.point;
            }

            lastshoot++;
            if(Input.GetKeyDown(KeyCode.I) && lastshoot>=speedNormal)
            {
               attaqueNormal();
                lastshoot=0;
            } 
            if(Input.GetKeyDown(KeyCode.O) && lastshoot>=speedDecors)
            {
               attaqueDecors();
                lastshoot=0;
            } 
            if(Input.GetKeyDown(KeyCode.P) && lastshoot>=speedSuper)
            {
               attaqueSuper();
                lastshoot=0;
            } 
        }

        }
        
    }

    public void attaqueNormal(){
        
        Vector3 aimDir= (mouseWorldPosition-spawnBulletPosition.position).normalized;
        Instantiate(pfBulletNormalProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    public void attaqueDecors(){
        
        Vector3 aimDir= (mouseWorldPosition-spawnBulletPosition.position).normalized;
        Instantiate(pfBulletDecorsProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    public void attaqueSuper(){
        
        Vector3 aimDir= (mouseWorldPosition-spawnBulletPosition.position).normalized;
        Instantiate(pfBulletSuperProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    

    public void ApplyDammage(){
        health--;
        if(health<=0)
        {
            Dead();
        }
    }
    public void Dead(){
        Debug.Log("poo");
        isDead=true;
        //Application.Quit();
    }
}
