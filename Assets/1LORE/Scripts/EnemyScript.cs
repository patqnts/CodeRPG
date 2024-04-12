using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public InventoryWeaponCheck weaponCheck;
    public Animator animator;
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

    public void Death()
    {
        animator.Play("Death");
    }

    public void DisabeSelf()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.Play("Attack");
        }
        
    }
}
