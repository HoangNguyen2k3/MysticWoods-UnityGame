using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageStoneBossMap : MonoBehaviour
{
    private GameObject boss;
    [SerializeField] private GameObject boss_pref;
    [SerializeField] private GameObject port;
    [SerializeField] private Transform port_position;
    // Start is called before the first frame update
    void Start()
    {
        boss= boss_pref.GetComponent<BossMechaStone>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            GameObject port_preb= Instantiate(port, port_position.position,Quaternion.identity);
            if(port_preb != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
