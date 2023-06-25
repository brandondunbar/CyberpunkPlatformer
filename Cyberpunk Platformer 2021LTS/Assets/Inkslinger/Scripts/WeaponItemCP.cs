using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New WeaponItem", menuName = "Custom/WeaponItem")]
public class WeaponItemCP : WeaponItem
{

    
    


    public override bool Equip(string playerID)
    {
        //This code runs when weapon is equipped

        base.Equip(playerID);
        //Put a pin in this (Work on weapon ability):
        //Add code to run when weaponCP is equipped (Currently only works in UI,  Need to fix- Might try to force current weapon in the HandleWeapon Class)
        return true;
    }





}
