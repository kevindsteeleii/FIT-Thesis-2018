using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl :Singleton<BulletControl>
{
    public Sprite[] Shots;
    public Image BulletImage;

    void Update()
    {
        try
        {
            // Debug.Log("Bullets available "+ Ammo.bullets);
            BulletImage.sprite = Shots[Ammo.instance.bullets];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new Exception ("No more bullets");
        }
    }
}
