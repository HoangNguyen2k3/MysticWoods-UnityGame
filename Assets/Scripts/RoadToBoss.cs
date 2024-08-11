using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadToBoss : MonoBehaviour
{
    private bool isPlayerInZone = false;
    private Coroutine checkStayCoroutine = null;

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
        SceneManager.LoadScene("Scene5");
    }
}