using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;
    public float maxViewAngle;
    public float minViewAngle;
    private bool noView;


    //occlusion
    public float zoomSpeed = 2f;
    public Transform Obstruction;
    public Transform CurrentObject;
    

    public bool invertY;
    // Start is called before the first frame update
    void Start()
        
    {
        noView = false;
        Obstruction = pivot;
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
   


    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.deltaTime != 0f)
        {
            //get the x position on the  mouse A rotate the target
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            target.Rotate(0, horizontal, 0);
            //get the y position of mouse and rotate pivot
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            pivot.Rotate(-vertical, 0, 0);
            if (invertY)
            {
                pivot.Rotate(vertical, 0, 0);
            }
            else
            {
                pivot.Rotate(-vertical, 0, 0);
            }

            //Limit up/down camera rotation
            if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
            {
                pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
            }
            if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
            {
                pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
            }

            //move the camera based on the current rotation of the target and offset
            float desireYangle = target.eulerAngles.y;
            float desireXangle = pivot.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(desireXangle, desireYangle, 0);
            transform.position = target.position - (rotation * offset);


            //transform.position = target.position - offset;

            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
            }
            transform.LookAt(target);
            ViewObstructed();
        }
    }

    public void fakeTarget(GameObject newtarget)
    {
        target = newtarget.transform;
    }

    //OCCLUSION
   void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, pivot.position - transform.position, out hit, 4f))
        {
            if(hit.collider.gameObject.tag != "Player"  && hit.collider.gameObject.tag == "Wall" && noView == false )
            {
                
                Obstruction = hit.transform;
                CurrentObject = Obstruction;
                noView = true;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if(Vector3.Distance(Obstruction.position, transform.position) >= 2.5f && Vector3.Distance(transform.position,target.position) >= 1f)
                {
                    
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    Debug.Log("sono entrato nell if col false e mettto il collider attuale in OFF");
                }
                
            }
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag == "Wall" && noView == true && hit.transform == CurrentObject)
            {

                Obstruction = hit.transform;
                
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                if (Vector3.Distance(Obstruction.position, transform.position) >= 2.5f && Vector3.Distance(transform.position, target.position) >= 1f)
                {

                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    Debug.Log("sono entrato nell if col true mettto il collider attuale in OFF");
                }

            }

            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag == "Wall" &&  noView == true && CurrentObject != hit.transform)
            {
                /*Debug.Log("Sono entrato nell'else if e adesso metto il collider precedente in ON");
                CurrentObject.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                CurrentObject = hit.transform;
                if (CurrentObject == hit.transform)
                {
                    Debug.Log("Ho corretto l'object");
                }*/
                CurrentObject.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                Debug.Log("Ho messo il vecchio collider on");
                Obstruction = hit.transform;
                CurrentObject = Obstruction;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                Debug.Log("Metto l'attuale collider off");

            }
            if (hit.collider.gameObject.tag == "Player")
            { 
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, pivot.position) < 4f)
                {
                    Debug.Log("Esco dal collider e metto ON");
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                    noView = false;
                }
            }
        } 
    }
}
