using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SinagScript : MonoBehaviour
{
    public int Health;

    public static SinagScript instance;

    private string savePath;

    private void Awake()
    {
        instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    void Start()
    {
        LoadPlayerData();
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
        if (File.Exists(savePath))
        {
            Debug.Log(savePath);
            string json = File.ReadAllText(savePath);
            SinagData sinag = JsonUtility.FromJson<SinagData>(json);
            Health = sinag.Health;
            this.transform.position = sinag.playerPos;
        }
    }
}
