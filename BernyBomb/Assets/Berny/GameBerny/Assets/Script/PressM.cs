using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressM : MonoBehaviour
{
    bool iscanv = false;
    private void Start()
    {
        StartCoroutine(ExecuteAfterTimeW(5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (iscanv == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void setcanv()
    {
        iscanv = true;
    }

    IEnumerator ExecuteAfterTimeW(float time)
    {
        yield return new WaitForSeconds(time);

        Time.timeScale = 0f;
    }
}
