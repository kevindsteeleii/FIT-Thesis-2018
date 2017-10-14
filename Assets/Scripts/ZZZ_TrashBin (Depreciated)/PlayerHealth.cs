using System;
using System.Collections;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{

    [Tooltip("Sets Player Health")]
    [Range(1, 100)]
    public int currentHP;

    public static int maxHP;

    public bool dead;
    bool invincible;
    PlayerController player;

    [Tooltip("Seconds of IFrames indicated by blinking")]
    [Range(2, 8)]
    public float waitTime;

    Renderer rendy;

    // Use this for initialization
    void Awake()
    {
        player = this.GetComponent<PlayerController>();
        maxHP = currentHP;
        invincible = false;
    }


    //handles damage taken by player character and its effects
    public void takeDamage(int dmg)
    {
        if (!invincible)
        {
            currentHP -= dmg;
            if (currentHP <= 0)
            {
                currentHP = 0;
                GameManager.instance.GameOver();
            }
            else
            {
                invincible = true;
            }

        }
    }


    public void addHealth(int heal)
    {
        if (currentHP + heal >= maxHP)
        {
            currentHP = maxHP;
        }

        else
        {
            currentHP += heal;
        }
    }


    /// <summary>
    /// Handles I-Frames as blinking character to indicate invincibility status
    /// </summary>
    /// <returns></returns>
    IEnumerator iFrames()
    {
        //still need a way to make the model flicker adn not just the surface application will fix later
        Color oldColor;
        Color newColor = new Color(255, 255, 255, 0);
        rendy = this.gameObject.GetComponent<Renderer>();
        oldColor = rendy.material.color;
        for (int i = 0; i < waitTime *5; i++)
        {
            rendy.material.color = newColor;
            yield return new WaitForSecondsRealtime(.1f);
            rendy.material.color = oldColor;
            yield return new WaitForSecondsRealtime(.1f);
        }
        invincible = false;
        // yield return null;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine(iFrames());
            takeDamage(col.gameObject.GetComponent<Enemy>().damage);
            Debug.Log("I've been hit!");
            
        }
    }
}
