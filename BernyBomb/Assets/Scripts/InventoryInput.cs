using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInput : MonoBehaviour
{
    [SerializeField] GameObject InventoryGameObject;
    public PlayerMovementEasy2 plmov;
    bool ihaveb = false;

    void Start()
    {
        if (InventoryGameObject.activeSelf)
        {
            InventoryGameObject.SetActive(!InventoryGameObject.activeSelf);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //for(int i=0; i<ToggleInventoryKeys.Length; i++)
        //{
            //if (Input.GetKeyDown(ToggleInventoryKeys[i]))
            if(Input.GetKeyDown(KeyCode.B) && ihaveb == true)
            {
                InventoryGameObject.SetActive(!InventoryGameObject.activeSelf);
                FindObjectOfType<AudioManager>().Play("blueprint");

            if (InventoryGameObject.activeSelf)
                {
                    ShowMouseCursor();
                    //Time.timeScale = 0f;
                    plmov.stopTito();
                }
                else
                {
                    HideMouseCursor();
                    //Time.timeScale = 1f;
                    plmov.playTito();
                };
                //break;
            };
        //}
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangeBool()
    {
        ihaveb = true;
    }
}
