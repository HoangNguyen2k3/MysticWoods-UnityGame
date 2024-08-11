using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKey : MonoBehaviour
{
    GameObject key;
    private void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
    }
    void Update()
    {
        if (ApplicationVariables.taked_key == false)
        {
            key.SetActive(false);
        }
        else
        {
            key.SetActive(true);
        }
    }
}
