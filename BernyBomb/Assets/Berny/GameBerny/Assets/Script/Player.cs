using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public float knockBackForce;
    public float knockMackTime;
    private float knockBackCounter;

    public AudioSource jumpSound;

    public GameObject fakeplayer;
    public CameraMove camera;
    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()

    {

        if(Input.GetKeyDown(KeyCode.U))
        {
            gameObject.SetActive(false);
            fakeplayer.SetActive(true);
            camera.fakeTarget(fakeplayer);
        }

        if (knockBackCounter <= 0)
        {

         //theRB.velocity = new Vector3(Input.GetAxis("Horizontal")* moveSpeed,theRB.velocity.y,Input.GetAxis("Vertical")*moveSpeed);

        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        }*/
        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded )
        {
            moveDirection.y = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    jumpSound.Play();
                moveDirection.y = jumpForce;
            }
        }
        } else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        anim.SetBool("isGrounded", controller.isGrounded);
        //anim.SetFloat("run", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        if (Input.GetAxis("Vertical") <0f)
        {
            anim.SetFloat("back",Input.GetAxis("Vertical"));
        } 
        if(Input.GetAxis("Vertical")>0f)
        {
            anim.SetFloat("run", Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") < 0f )
        {
            anim.SetFloat("left", Input.GetAxis("Horizontal"));
            
        }
        else
        {
            anim.SetFloat("right", Input.GetAxis("Horizontal"));
        }
        if(Input.GetMouseButton(0))
        {
            anim.SetBool("shoot", true);
           
        } else if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("shoot", false);
        }
    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockMackTime;


        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

}
