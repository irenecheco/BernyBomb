using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HealtManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Player thePlayer;

    public float invincibilityLenght;
    private float invincibilityCounter;

    public Renderer playerRend;
    private float flashCounter;
    public float flashLenght = 0.1f;
    private bool isRespawing;
    private Vector3 respownPoint;
    public float respawnLenght;

    public GameObject deathEffect;
    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        //thePlayer = FindObjectOfType<Player>();

        respownPoint = thePlayer.transform.position;
            
            }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.transform.position.y < -20.0f)
        {
          
                Respawn();
             
            
            

        }
        if (invincibilityCounter >0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <=0)
            {
                playerRend.enabled = !playerRend.enabled;
                flashCounter = flashLenght;
            }

            if(invincibilityCounter <= 0)
            {
                playerRend.enabled = true;

            }
        }
        if(isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }

    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if(invincibilityCounter <=0)
        {
            currentHealth -= damage;
            FindObjectOfType<GameManager>().MinusLife(damage);
             
            if(currentHealth <= 0)
            {
                Respawn();
            } else
            {

            thePlayer.Knockback(direction);

            invincibilityCounter = invincibilityLenght;

            playerRend.enabled = false;

            flashCounter = flashLenght;
            
            }
        }
    }

 

    public void Respawn()
    {
        //thePlayer.transform.position = respownPoint;
        //currentHealth = maxHealth;
        if (!isRespawing)
        {
            StartCoroutine("RespawnCo");
        }

    }

    public IEnumerator RespawnCo()
    {
        isRespawing = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLenght);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawing = false;

        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respownPoint;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLenght;
        playerRend.enabled = false;
        flashCounter = flashLenght;
        
        FindObjectOfType<GameManager>().AddLife(3);
       
    }

    public void HealthPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
    }
    public void AddCurrentLife(int lifeToAdd)
    {
        currentHealth += lifeToAdd;
       
    }
}
