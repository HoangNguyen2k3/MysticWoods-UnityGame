using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackSpecialSkill : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject skillQ;
    [SerializeField] private GameObject skillE;
    private bool skillQCanAttack = true;
    private bool skillECanAttack = true;
    [SerializeField] private Transform SpawnSkillQ;
    [SerializeField] private Transform SpawnSkillE;
    [SerializeField] private float skillQCooldown = 10f; 
    [SerializeField] private float skillECooldown = 15f;
    [SerializeField] private Button imageSkillQ;
    [SerializeField] private Button imageSkillE;
    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    void Start()
    {
        playerController.SpecialSkill.Special1.performed += _ => SkillQAttack();
        playerController.SpecialSkill.Special2.performed += _ => SkillEAttack();
    }

    private void SkillQAttack()
    {
        if (skillQCanAttack)
        {
            Instantiate(skillQ, SpawnSkillQ.position, Quaternion.identity);
            StartCoroutine(WaitToReCallSkillQ());
        }
    }

    private void SkillEAttack()
    {
        if (skillECanAttack)
        {
            Instantiate(skillE, SpawnSkillE.position, Quaternion.identity);
            StartCoroutine(WaitToReCallSkillE());
        }
    }

    private IEnumerator WaitToReCallSkillQ()
    {
        skillQCanAttack = false;
        imageSkillQ.interactable = false;
        yield return new WaitForSeconds(skillQCooldown);
        skillQCanAttack = true;
        imageSkillQ.interactable = true;
    }

    private IEnumerator WaitToReCallSkillE()
    {
        skillECanAttack = false;
        imageSkillE.interactable = false;
        yield return new WaitForSeconds(skillECooldown);
        skillECanAttack = true;
        imageSkillE.interactable = true;
    }
}
