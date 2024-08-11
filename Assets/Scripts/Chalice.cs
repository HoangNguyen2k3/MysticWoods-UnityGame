using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalice : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Playercontroller>() != null)
        {
            ApplicationVariables.taked_chaliced = true;
            Destroy(gameObject);
        }
    }


    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-2f, 2f);

        Vector2 endPoint = new Vector2(randomX, randomY);
        float timePassed = 0f;
        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
}
