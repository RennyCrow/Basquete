﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VERIFICA_AREA_RESTRITA : MonoBehaviour {

    public static bool restrita = false;

    private void OnMouseOver()
    {
        if(restrita == false)
        {
            restrita = true;
            print(restrita);
        }

    }

    private void OnMouseExit()
    {
        if (restrita == true)
        {
            restrita = false;
            print(restrita);
        }
    }

}
