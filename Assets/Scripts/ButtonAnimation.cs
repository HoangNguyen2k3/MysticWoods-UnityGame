using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    Button btn;
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1.2f);

    private void Awake()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(Anim);
    }
    private void Anim()
    {
        LeanTween.scale(gameObject,upScale,0.1f);
        LeanTween.scale(gameObject, Vector3.one, 0.1f).setDelay(0.1f);
    }
}
