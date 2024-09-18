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
    [SerializeField] private bool isSkillMouseQ = false;
    [SerializeField] private bool isSkillMouseE = false;
    [SerializeField] private bool isSkillDirectionMouseQ = false;
    [SerializeField] private bool isSkillDirectionMouseE = false;
    CoolDownSkill skillCoolDown;
    private void Awake()
    {
        playerController = new PlayerController();
        skillCoolDown = gameObject.GetComponentInParent<CoolDownSkill>();
    }
    private void Update()
    {
        if (SpawnSkillE==null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                SpawnSkillE = GameObject.FindGameObjectWithTag("Player").transform;
            }
            
        }
        if(SpawnSkillQ==null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                SpawnSkillQ= GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
    private void OnEnable()
    {
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
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
            if (isSkillMouseQ)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f;
                Instantiate(skillQ, worldPosition, Quaternion.identity);
            }
            else if (isSkillDirectionMouseQ)
            {
                Debug.Log(SpawnSkillQ.position);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f;
                Vector3 direction = (worldPosition - SpawnSkillQ.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Instantiate(skillQ, SpawnSkillQ.position, Quaternion.Euler(0, 0, angle));
            }
            else
            {
                Instantiate(skillQ, SpawnSkillQ.position, Quaternion.identity);
            }
            
            StartCoroutine(WaitToReCallSkillQ());
        }
    }

    private void SkillEAttack()
    {
        if (skillECanAttack)
        {
            if (isSkillMouseE)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f;
                Instantiate(skillE, worldPosition, Quaternion.identity);
            }else if (isSkillDirectionMouseE)
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPosition.z = 0f;
                Vector3 direction = (worldPosition - SpawnSkillE.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Instantiate(skillE, SpawnSkillE.position, Quaternion.Euler(0, 0, angle));
            }
            else
            {

                Instantiate(skillE, SpawnSkillE.position, Quaternion.identity);
            }
            
            StartCoroutine(WaitToReCallSkillE());
        }
    }

    private IEnumerator WaitToReCallSkillQ()
    {
        skillQCanAttack = false;
        imageSkillQ.interactable = false;
        skillCoolDown.StartCoolDownQ(skillQCooldown) ;
        yield return new WaitForSeconds(skillQCooldown);
        skillQCanAttack = true;
        imageSkillQ.interactable = true;
    }

    private IEnumerator WaitToReCallSkillE()
    {
        skillECanAttack = false;
        imageSkillE.interactable = false;
        skillCoolDown.StartCoolDownE(skillECooldown);
        yield return new WaitForSeconds(skillECooldown);
        skillECanAttack = true;
        imageSkillE.interactable = true;
    }
}
