using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathscript : MonoBehaviour
{
    public void ConfirmButton()
    {
        SinagScript.instance.Respawn();
        Destroy(gameObject);
    }
}
