using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKey : MonoBehaviour
{
    [SerializeField] private GameObject keyObj;
    [SerializeField] private GameObject chaliceObj;
    [SerializeField] private Transform positionKey;
    GameObject key;
    GameObject chalice;
    private void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
        chalice = GameObject.FindGameObjectWithTag("Chalice");
       // Debug.Log(ApplicationVariables.taked_chaliced);
    }
    void Update()
    {
        if(key == null&&ApplicationVariables.taked_key==false) {
            return;
        }
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
        if (chalice == null && ApplicationVariables.taked_chaliced == true)
        {
            chalice = Instantiate(chaliceObj, positionKey.position, Quaternion.identity);
        }
        if (ApplicationVariables.taked_chaliced == false)
        {

            chalice.SetActive(false);
        }
        else
        {
            chalice.SetActive(true);
        }
    }

}
