using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovementEasy2 : MonoBehaviour
{
    CharacterController cc;
    private float speed = 0.16f;
    float ySpeed = 0f;
    float gravity = -15f;
    public Transform fpsCamera;
    float pitch = 0f;

    [Range(5, 15)]
    public float mouseSensitivity = 6f;

    [Range(45, 85)]
    public float pitchRange = 45f;

    float xInput = 0f;
    float zInput = 0f;
    float xMouse = 0f;
    float yMouse = 0f;

    //Trigger InteractMsg
    public InteractMsg Interactmsg;
    public ScatolinaGame ScatolinaGame;
    public LockerGame LockerGame;
    private GameObject toPickup_;
    public Transform Dest;
    public Image Martelloui;
    public Image Keyui;
    public GameObject GridCabinato;
    public GameObject SalvRotto;
    public GameObject BPlane;
    public GameObject Cabinato;
    public GameObject DisplayC;
    public FillProgressBar fillBar;
    public GameObject PlugIn;
    public GameObject Plug_Slot;
    public GameObject progBar;
    public InventoryInput invent;
    public GameObject UITito;
    public GameObject Inventory;
    public Text load;

    public Material Material1;

    GameObject[] NotColl;
    bool moveTito = false;
    bool isOpen = false;
    bool plugged = false;
    bool caninsert = false;
    bool bluemsg = true;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        NotColl = GameObject.FindGameObjectsWithTag("NotCollider");
        foreach (GameObject NotColl in NotColl)
        {
            Physics.IgnoreCollision(NotColl.GetComponent<Collider>(), GetComponent<Collider>());
        }
        BPlane.GetComponentInChildren<Image>().DOFade(0.0f, 0f);
        DisplayC.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && toPickup_ != null)
        {
            pickedUp(toPickup_);
            Interactmsg.CloseMessagePanel();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ExecuteAfterTimeFinal(0.5f));
        }

        if (Time.deltaTime == 0 || moveTito == false)
        {
            return;
        }
        else
        {
            GetInput();
            UpdateMovement();
        }

        if (Cabinato.transform.Find("Cabinato_Bottoni_C").gameObject.activeSelf
                    && Cabinato.transform.Find("Cabinato_Cassa_L_C").gameObject.activeSelf
                    && Cabinato.transform.Find("Cabinato_Display_C").gameObject.activeSelf
                    && caninsert == false)
        {
            caninsert = true;
            Plug_Slot.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        xMouse = Input.GetAxis("Mouse X") * mouseSensitivity;
        yMouse = Input.GetAxis("Mouse Y") * mouseSensitivity;

        /*if (xInput != 0 || zInput != 0)
        {
            GetComponent<Animator>().SetBool("walk", true);
        }
        else if(xInput == 0 && zInput == 0)
        {
            GetComponent<Animator>().SetBool("walk", false);
        }*/
        
        if (xInput != 0 || zInput != 0)
        {
            FindObjectOfType<AudioManager>().PlayOneShot("steps");
        }
        else if (xInput == 0 && zInput == 0)
        {
            FindObjectOfType<AudioManager>().Stop("steps");
        }
    }
    void UpdateMovement()
    {
        Vector3 move = new Vector3(xInput, 0, zInput);
        move = Vector3.ClampMagnitude(move, speed);
        move = transform.TransformVector(move);

        if (cc.isGrounded)
        {
            ySpeed = gravity * Time.deltaTime;
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }
        cc.Move(move + new Vector3(0, ySpeed, 0) * Time.deltaTime);

        transform.Rotate(0, xMouse, 0);
        pitch -= yMouse;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange);
        Quaternion camRotation = Quaternion.Euler(pitch, 0, 0);
        fpsCamera.localRotation = camRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(bluemsg == true)
        {
             if (other.gameObject.name == "Blueprint")
            {
                Interactmsg.OpenMessagePanel("Pick up with E");
                toPickup_ = other.gameObject;
            }
        }
        else
        {
            if (other.gameObject.name == "Porta")
            {
                Interactmsg.OpenMessagePanel("It's locked");
                toPickup_ = other.gameObject;
                FindObjectOfType<AudioManager>().Play("door");
            }
            else if (other.gameObject.name == "Martello" || other.gameObject.name == "Key")
            {
                Interactmsg.OpenMessagePanel("Pick up with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Baule" && bluemsg == false)
            {
                if (Keyui.enabled == false)
                {
                    Interactmsg.OpenMessagePanel("Find key to open");
                }
                else if (Keyui.enabled == true)
                {
                    Interactmsg.OpenMessagePanel("Use key with E");
                    toPickup_ = other.gameObject;
                }
            }
            else if (other.gameObject.name == "Salvadanaio")
            {
                if (Martelloui.enabled == false)
                {
                    Interactmsg.OpenMessagePanel("Find hammer to break");
                }
                else if (Martelloui.enabled == true)
                {
                    Interactmsg.OpenMessagePanel("Use hammer with E");
                    toPickup_ = other.gameObject;
                }
            }
            else if (other.gameObject.name == "Locker")
            {
                Interactmsg.OpenMessagePanel("Open the lock with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Scatolina")
            {
                Interactmsg.OpenMessagePanel("Open with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Cabinato")
            {
                if (Dest.childCount != 0)
                {
                    Interactmsg.OpenMessagePanel("Insert with E");
                    toPickup_ = Dest.gameObject.transform.GetChild(0).gameObject;
                }
                else if (Dest.childCount == 0)
                {
                    if (Cabinato.transform.Find("Cabinato_Bottoni_C").gameObject.activeSelf
                        && Cabinato.transform.Find("Cabinato_Cassa_L_C").gameObject.activeSelf
                        && Cabinato.transform.Find("Cabinato_Display_C").gameObject.activeSelf)
                    {
                        if (PlugIn.activeSelf)
                        {
                            Interactmsg.OpenMessagePanel("Insert coin to start");
                        }
                        else
                        {
                            Interactmsg.OpenMessagePanel("Find the plug");
                        }
                    }
                    else
                    {
                        Interactmsg.OpenMessagePanel("Find the missing parts");
                    }
                }
            }
            else if (other.gameObject.name == "SpinaUnplugged")
            {
                if (caninsert == true)
                {
                    Interactmsg.OpenMessagePanel("Plug in with E");
                    toPickup_ = other.gameObject;
                }
            }
            else if (other.gameObject.name == "Cabinato_Bottoni")
            {
                Interactmsg.OpenMessagePanel("Pick up buttons with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Cabinato_Cassa_L")
            {
                Interactmsg.OpenMessagePanel("Pick up speaker with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Cabinato_Display")
            {
                Interactmsg.OpenMessagePanel("Pick up monitor with E");
                toPickup_ = other.gameObject;
            }
            else if (other.gameObject.name == "Moneta")
            {
                Interactmsg.OpenMessagePanel("Pick up coin with E");
                toPickup_ = other.gameObject;
            }
            else
            {
                toPickup_ = other.gameObject;
                Physics.IgnoreCollision(other, GetComponent<Collider>());
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        Interactmsg.CloseMessagePanel();
        toPickup_ = null;
    }

    public void pickedUp(GameObject toPickup)
    {
        if(toPickup.name == "Martello")
        {
            toPickup.SetActive(false);
            Martelloui.enabled = true;
            FindObjectOfType<AudioManager>().Play("interact");
        }
        else if (toPickup.name == "Key")
        {
            toPickup.SetActive(false);
            Keyui.enabled = true;
            FindObjectOfType<AudioManager>().Play("interact");
        }
        else if (toPickup.name == "Blueprint")
        {
            toPickup.SetActive(false);
            progBar.SetActive(true);
            invent.ChangeBool();
            bluemsg = false;
            FindObjectOfType<AudioManager>().Play("blueprint");
        }
        else if (toPickup.name == "Salvadanaio")
        {
            DOTween.Sequence()
                .Append(BPlane.GetComponentInChildren<Image>().DOFade(1.0f, 0.9f).SetEase(Ease.OutQuad))
                .Append(BPlane.GetComponentInChildren<Image>().DOFade(0.0f, 0.9f).SetEase(Ease.InQuad));
            StartCoroutine(ExecuteAfterTimeS(0.8f, toPickup));
            Martelloui.enabled = false;
        }
        else if (toPickup.name == "Baule")
        {
            isOpen = true;
            toPickup.GetComponent<Animator>().SetBool("open", isOpen);
            DisplayC.GetComponent<BoxCollider>().enabled = true;
            Keyui.enabled = false;
            NoCollision(toPickup);
            FindObjectOfType<AudioManager>().Play("chest_o");
        }
        else if (toPickup.name == "Scatolina")
        {
            ScatolinaGame.PlayBox();
        }
        else if (toPickup.name == "Locker")
        {
            LockerGame.PlayLock();
        }
        else if (toPickup.name == "Cabinato_Bottoni")
        {
            if (toPickup.GetComponent<Rigidbody>().useGravity == false)
            {
                toPickup.transform.parent = null; 
                toPickup.SetActive(false);
                Cabinato.transform.Find(toPickup.name + "_C").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Play("interact");
            }
            else
            {
                toPickup.SetActive(false);
                GridCabinato.transform.Find("Btn_Slot").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                fillBar.FillUp();
                toPickup_ = null;
            }
        }
        else if (toPickup.name == "Cabinato_Cassa_L")
        {
            if (toPickup.GetComponent<Rigidbody>().useGravity == false)
            {
                toPickup.transform.parent = null;
                toPickup.SetActive(false);
                Cabinato.transform.Find(toPickup.name + "_C").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Play("interact");
            }
            else
            {
                toPickup.SetActive(false);
                GridCabinato.transform.Find("Cassa_Slot").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                fillBar.FillUp();
                toPickup_ = null;
            }
        }
        else if (toPickup.name == "Cabinato_Display")
        {
            if (toPickup.GetComponent<Rigidbody>().useGravity == false)
            {
                toPickup.transform.parent = null;
                toPickup.SetActive(false);
                Cabinato.transform.Find(toPickup.name + "_C").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Play("interact");
            }
            else
            {
                toPickup.SetActive(false);
                GridCabinato.transform.Find("Display_Slot").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                fillBar.FillUp();
                toPickup_ = null;
            }
        }
        else if (toPickup.name == "Moneta")
        {
            if (toPickup.GetComponent<Rigidbody>().useGravity == false)
            {
                toPickup.transform.parent = null;
                toPickup.SetActive(false);
                StartCoroutine(ExecuteAfterTimeFinal(0.7f));
                FindObjectOfType<AudioManager>().Play("coin");
            }
            else
            {
                toPickup.SetActive(false);
                GridCabinato.transform.Find("Coin_Slot").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                fillBar.FillUp();
                toPickup_ = null;
            }
        }
        else if (toPickup.name == "SpinaUnplugged")
        {
            DOTween.Sequence()
                .Append(BPlane.GetComponentInChildren<Image>().DOFade(1.0f, 0.9f).SetEase(Ease.OutQuad))
                .Append(BPlane.GetComponentInChildren<Image>().DOFade(0.0f, 0.9f).SetEase(Ease.InQuad));
            StartCoroutine(ExecuteAfterTimeP(0.8f, toPickup));
            plugged = true;
            Cabinato.transform.Find("Cabinato_Display_C").gameObject.GetComponent<MeshRenderer>().material = Material1;
            toPickup_ = null;
        }
        else
        {
        }
    }

    public void stopTito()
    {
        moveTito = false;
    }
    public void playTito()
    {
        moveTito = true;
    }

    IEnumerator ExecuteAfterTimeS(float time, GameObject toPickup)
    {
        yield return new WaitForSeconds(time);

        toPickup.SetActive(false);
        SalvRotto.SetActive(true);
        FindObjectOfType<AudioManager>().Play("pig_break");
    }

    IEnumerator ExecuteAfterTimeP(float time, GameObject toPickup)
    {
        yield return new WaitForSeconds(time);

        toPickup.SetActive(false);
        PlugIn.SetActive(true);
        FindObjectOfType<AudioManager>().Play("plug");
    }

    IEnumerator ExecuteAfterTimeFinal(float time)
    {
        yield return new WaitForSeconds(time);

        BPlane.GetComponentInChildren<Image>().DOColor(new Color32(255, 255, 255, 255), 2.0f).SetEase(Ease.OutCubic);
        load.DOFade(1.0f, 2.0f);
        UITito.SetActive(false);
        Inventory.SetActive(false);
        FindObjectOfType<AudioManager>().Play("portal");
        FindObjectOfType<AudioManager>().Fade2("Theme");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NoCollision(GameObject notcoll)
    {
        Physics.IgnoreCollision(notcoll.GetComponent<Collider>(), GetComponent<Collider>());
    }

    public bool RetPlugged()
    {
        return plugged;
    }
}
