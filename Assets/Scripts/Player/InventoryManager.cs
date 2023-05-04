using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];

    public List<AbilityItem> abilities = new List<AbilityItem>();

    public List<GameObject> souls = new List<GameObject>();

    public List<GameObject> foodItems = new List<GameObject>();

    bool usingAbility;

    public void AddAbility(AbilityItem i)
    {
        abilities.Add(i);
    }

    public bool HasAbilityItem()
    {
        return abilities.Count > 0;
    }

    public void UseAbility()
    {
        AbilityItem item = abilities[0];
        abilities.Remove(item);
        item.ActivateAbility();
    }

    public bool IsUsingAbility()
    {
        return usingAbility;
    }

    public void SetUsingAbility(bool b)
    {

        usingAbility = b;
    }

    public void AddSoul(GameObject s)
    {
        souls.Add(s);
    }

    public void RemoveSoul(GameObject s)
    {
        souls.Remove(s);
    }

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weapon.Level;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem item)
    {
        passiveItemSlots[slotIndex] = item;
        passiveItemLevels[slotIndex] = item.passiveItemData.Level;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];
            if (!weapon.weapon.NextLevelPrefab)
            {
                Debug.Log("No next level set for " + weapon.name);
                return;
            }
            GameObject upgradedWeapon = Instantiate(weapon.weapon.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weapon.Level;
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if(passiveItemSlots.Count > slotIndex)
        {
            PassiveItem item = passiveItemSlots[slotIndex];
            if (!item.passiveItemData.NextLevelPrefab)
            {
                Debug.Log("No next level set for " + item.name);
                return;
            }
            GameObject upgradedItem = Instantiate(item.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedItem.GetComponent<PassiveItem>());
            Destroy(item.gameObject);
            weaponLevels[slotIndex] = upgradedItem.GetComponent<PassiveItem>().passiveItemData.Level;
        }
    }


    
}
