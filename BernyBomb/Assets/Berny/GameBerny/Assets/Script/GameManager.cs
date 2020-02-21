using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentBomb;
    public Text bombText;
    public Text lifeText;
    public int currentLife;
    public AudioSource audioBomb;
    public Image introImage;
    public GameObject berny;
 
  

    // Start is called before the first frame update

    
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if (currentLife <= 0)
        {
            
            lifeText.text = "X " + 3;
            
        }
        else if(berny.transform.position.y < -20.0f)
        {
            currentLife = 0;
            lifeText.text = "X " + 3;
        }
    }

  
    public void AddGold(int goldToAdd)
    {
        currentBomb += goldToAdd;
        bombText.text = "X " + currentBomb;
        audioBomb.Play();
        if (currentBomb >= 10)
        {
            currentBomb = 0;
            bombText.text = "X " + currentBomb;
            FindObjectOfType<GameManager>().AddLife(1);
            FindObjectOfType<HealtManager>().AddCurrentLife(1);
        }
    }
    public void MinusLife(int lifeToLeave)
    {
        currentLife -= lifeToLeave;
        lifeText.text = "X " + currentLife;
       
    }
    public void AddLife(int lifeToAdd)
    {
        currentLife += lifeToAdd;
        lifeText.text = "X " + currentLife;
    }

}
