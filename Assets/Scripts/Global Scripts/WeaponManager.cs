using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //static members belong to the class itself and are shared across all class instances.
    public static Weapon ActiveWeapon { get; set; } //The default weapon is the Lacelerator Beam
    public static int WeaponsInHand { get; set; } = 1; //Initially you have one weapon. Find more weapons in the game.

    public enum Weapon { LaceleratorBeam, ShotBeam, MissileLauncher }; //There are only three player weapons. They are thus defined in an enum.

    private void Update()
    {
        //if(Input.GetButtonDown("Change Weapons") && WeaponsInHand > 1)
        if (Input.GetButtonDown("Change Weapons"))
        {
            switch (ActiveWeapon)
            {
                case Weapon.LaceleratorBeam:
                    ActiveWeapon = Weapon.ShotBeam;
                    break;

                case Weapon.ShotBeam:
                    ActiveWeapon = Weapon.MissileLauncher;
                    break;

                case Weapon.MissileLauncher:
                    ActiveWeapon = Weapon.LaceleratorBeam;
                    break;
            }
        }
    }
}
