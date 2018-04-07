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
        Ammo.instance.On_AmmoChanged_Sent += On_AmmoChanged_Received;
    }

    void On_AmmoChanged_Received(int ammo)
    {
        if (ammo >= 0)
        {
            BulletImage.sprite = Shots[ammo];
        }
        else
            throw new ArgumentOutOfRangeException("Out of Bullets");

    }
}
