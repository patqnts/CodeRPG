using MoreMountains.InventoryEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWeaponCheck : MonoBehaviour
{
    public Inventory weaponInventory;
    void OnEnable()
    {
        Lua.RegisterFunction("IsWeaponEquipped_" + name, this, SymbolExtensions.GetMethodInfo(() => IsWeaponEquipped(name)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("IsWeaponEquipped");
    }
    public bool IsWeaponEquipped(string weaponID)
    {

        List<int> myList = weaponInventory.InventoryContains(weaponID);

        if (myList.Count > 0)
        {
            Debug.Log("EXISTS");
            DialogueLua.SetVariable("IsWeaponEquipped", true);
            return true;

        }
        Debug.Log("NOT EXISTS");
        DialogueLua.SetVariable("IsWeaponEquipped", false);
        return false;
    }
}
