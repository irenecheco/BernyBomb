using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public int startingHealth = 5;
    private int currentHealth;
    public GameObject gameObject;
    [SerializeField]
    private AudioSource defaultSong;
    public GameObject deathEffect;
    public AudioSource enemySoundDeath;
    public GameObject boss;
    public AudioSource fightSong;

    //public Image endImage;
    //public float endSpeed;
    public MoveDownWon movedown;

    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = startingHealth; 
    }

    public void TakeDamage (int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            enemySoundDeath.Play();
            Die();
                   
        }

    }
    private void Die()
    {

        Destroy(gameObject);
        fightSong.Pause();
        defaultSong.Play();

        if(gameObject.tag == "Boss")
        {
            movedown.setmove();
            defaultSong.Stop();
            //endImage.color = new Color(endImage.color.r, endImage.color.g, endImage.color.b, Mathf.MoveTowards(endImage.color.a, 1f, endSpeed * Time.deltaTime));
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
