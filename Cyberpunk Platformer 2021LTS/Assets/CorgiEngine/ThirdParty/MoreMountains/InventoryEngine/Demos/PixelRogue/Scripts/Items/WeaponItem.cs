using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;


namespace MoreMountains.InventoryEngine
{	
	[CreateAssetMenu(fileName = "WeaponItem", menuName = "MoreMountains/InventoryEngine/WeaponItem", order = 2)]
	[Serializable]
	/// <summary>
	/// Demo class for a weapon item
	/// </summary>
	public class WeaponItem : InventoryItem 
	{
		[Header("Weapon")]
		/// the sprite to use to show the weapon when equipped
		public Sprite WeaponSprite;

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool Equip(string playerID)
		{
			base.Equip(playerID);
            // Commented out code below because I'm pretty sure this is old code, it accesses a super basic Demo Character that has no use anymore
            //TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetWeapon(WeaponSprite, this);  

            return true;
		}

		/// <summary>
		/// What happens when the object is used 
		/// </summary>
		public override bool UnEquip(string playerID)
		{
			base.UnEquip(playerID);
            // Commented out code below because I'm pretty sure this is old code, it accesses a super basic Demo Character that has no use anymore
            //TargetInventory(playerID).TargetTransform.GetComponent<InventoryDemoCharacter>().SetWeapon(null,this);
            return true;
		}
		
	}
}