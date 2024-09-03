using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoolDownSkill : MonoBehaviour
{
    public float numCoolDownQ = 0;
    public float numCoolDownE = 0;
    [SerializeField] public TextMeshProUGUI skillQ;
    [SerializeField] public TextMeshProUGUI skillE;
    void Start()
    {
        skillE.text = "";
        skillQ.text = "";
    }

    // Update is called once per frame
    public void StartCoolDownQ(float cooldownTime)
    {
        StartCoroutine(CoolDownRoutineQ(cooldownTime));
    }

    public void StartCoolDownE(float cooldownTime)
    {
        StartCoroutine(CoolDownRoutineE(cooldownTime));
    }

    private IEnumerator CoolDownRoutineQ(float cooldownTime)
    {
        numCoolDownQ = cooldownTime;
        while (numCoolDownQ > 0)
        {
            skillQ.text = numCoolDownQ.ToString();
            yield return new WaitForSeconds(1f);
            numCoolDownQ--;
        }
        skillQ.text = "";
    }

    private IEnumerator CoolDownRoutineE(float cooldownTime)
    {
        numCoolDownE = cooldownTime;
        while (numCoolDownE > 0)
        {
            skillE.text = numCoolDownE.ToString();
            yield return new WaitForSeconds(1f);
            numCoolDownE--;
        }
        skillE.text = "";
    }

}
