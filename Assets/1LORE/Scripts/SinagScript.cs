using MoreMountains.InventoryEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SinagScript : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    public Inventory weapon;
    public Inventory main;
    public static SinagScript instance;
    public Text healthText;
    private string savePath;
    public GameObject deathScreen;
    private void Awake()
    {
        instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    void Start()
    {
    }

    public void SavePlayerData()
    {
        SinagData sinagData = new SinagData();
        sinagData.Health = Health;
        sinagData.playerPos = this.transform.position;

        string json = JsonUtility.ToJson(sinagData);
        File.WriteAllText(savePath, json);
        Debug.Log(savePath + " " + json);
    }

    public void LoadPlayerData()
    {

        PlayerController.player.LoadInventory();
        //RESET OR CONTINUE
        SaveTypeScript sts = FindObjectOfType<SaveTypeScript>();
        if (sts != null)
        {
            Debug.Log("IT EXISTS");
            if (sts.saveType == 0) // NEW GAME
            {
                ResetInventory(weapon);
                ResetInventory(main);
            }

            else if (File.Exists(savePath) && sts.saveType == 1)
            {
                Debug.Log(savePath);
                string json = File.ReadAllText(savePath);
                SinagData sinag = JsonUtility.FromJson<SinagData>(json);
                Health = sinag.Health;
                this.transform.position = sinag.playerPos;

                
                
            }
        }  
    }


    public void ResetInventory(Inventory inventory)
    {
        inventory.EmptyInventory();
    }


    public void TakeDamage(int damage)
    {
        if(Health > 0)
        {
            Health -= damage;
            
        }

        if(Health <= 0)
        {
            Debug.Log("Death");
            Health = 0;
            //Restart spawnpoint
        }
        healthText.text = $"{Health}/{MaxHealth}";
    }

    public void DeathMethod() 
    {

    }
}
