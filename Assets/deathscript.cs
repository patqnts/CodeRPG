using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathscript : MonoBehaviour
{
    public void ConfirmButton()
    {
        SinagScript.instance.Respawn();       
        PlayerController.player.moveSpeed = 3;
        Destroy(gameObject);
    }
}
