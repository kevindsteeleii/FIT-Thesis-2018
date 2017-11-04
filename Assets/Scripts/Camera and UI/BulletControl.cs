using System;
using UnityEngine;
using UnityEngine.UI;

public class BulletControl :Singleton<BulletControl>
{
    public Sprite[] Shots;
    public Image BulletImage;

    void Start()
    {
        //GetAmmo subscribes the event MyAmmo that updates the UI element
        Ammo.instance.MyAmmo += GetAmmo;
    }

    void GetAmmo(int ammo)
    {
        BulletImage.sprite = Shots[ammo];
        //if (ammo > 0)
        //{
        //    Debug.Log("Bullets available " + Ammo.bullets);
        //    BulletImage.sprite = Shots[ammo];
        //}
        //else
        //{
        //    Debug.Log("No more bullets");
        //}
    }
}
