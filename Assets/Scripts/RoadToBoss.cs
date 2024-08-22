using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadToBoss : MonoBehaviour
{
    private bool isPlayerInZone = false;
    private Coroutine checkStayCoroutine = null;
    [SerializeField] private GameObject boxTalk;

    private void Awake()
    {
        boxTalk.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlayerInZone)
        {
            isPlayerInZone = true;
            checkStayCoroutine = StartCoroutine(CheckPlayerStay(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInZone = false;
            if (checkStayCoroutine != null)
            {
                StopCoroutine(checkStayCoroutine);
                checkStayCoroutine = null;
            }
        }
    }

    private IEnumerator CheckPlayerStay(GameObject player)
    {
        yield return new WaitForSeconds(1.0f);

        if (isPlayerInZone&&ApplicationVariables.taked_chaliced==true)
        {
            StartMethod();
        }
    }

    private void StartMethod()
    {
        ApplicationVariables.taked_chaliced = false;
        SceneManager.LoadScene("Scene5");
    }
    private void Update()
    {
        if (ApplicationVariables.add_boss_skeleton < 0)
        {
            boxTalk.SetActive(true);
            ApplicationVariables.add_boss_skeleton = 0;
        }
    }
}
