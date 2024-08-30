using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class Health_player_skillE : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(HealingPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position=Playercontroller.Instance.transform.position;
    }
    private IEnumerator HealingPlayer()
    {

        AddCurrentHealth(2);
        yield return new WaitForSeconds(2f);
        AddCurrentHealth(2);
        yield return new WaitForSeconds(2f);
        AddCurrentHealth(1);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void AddCurrentHealth(int healthadd)
    {
        if((PlayerHealth.Instance.currentHealth+healthadd) <= ApplicationVariables.maxHealthCurrent) {
            PlayerHealth.Instance.currentHealth+=healthadd;
            PlayerHealth.Instance.UpdatHealthSlider();
        }else if((PlayerHealth.Instance.currentHealth + healthadd) > ApplicationVariables.maxHealthCurrent)
        {
            PlayerHealth.Instance.currentHealth=ApplicationVariables.maxHealthCurrent;
            PlayerHealth.Instance.UpdatHealthSlider();
        }
    }
}
