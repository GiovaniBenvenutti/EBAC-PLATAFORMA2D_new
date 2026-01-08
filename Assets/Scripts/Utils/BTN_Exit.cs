using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Exit : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quer sair");
        Application.Quit();
    }
}
