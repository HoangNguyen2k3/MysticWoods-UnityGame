using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKey : MonoBehaviour
{
    [SerializeField] private GameObject keyObj;
    [SerializeField] private Transform positionKey;
    GameObject key;
    private void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
    }
    void Update()
    {
        if (key == null&&ApplicationVariables.taked_key==true)
        {
            key=Instantiate(keyObj,positionKey.position,Quaternion.identity);
        }
        if (ApplicationVariables.taked_key == false||ApplicationVariables.taked_chaliced==true)
        {
            key.SetActive(false);
        }
        else
        {
            key.SetActive(true);
        }
    }

}
