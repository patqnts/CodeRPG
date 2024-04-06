using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public InventoryWeaponCheck weaponCheck;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        weaponCheck = GameObject.FindObjectOfType<InventoryWeaponCheck>();
    }

    public void DoMethod(string weaponID)
    {
        weaponCheck.IsWeaponEquipped(weaponID);
    }
}
