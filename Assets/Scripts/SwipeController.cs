using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;
    LTDescr tween;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button previousBtn,nextBtn;
    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        if (barClosed != null && barOpen != null)
        {
            UpdateBar();
        }
        UpdateArrowButton();
    }
    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }
    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }
    void MovePage() {
        if (tween != null)
        {
            tween.reset();
        }
        tween = levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        if (barClosed != null && barOpen != null)
        {
            UpdateBar();
        }
       
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }
    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }
    void UpdateArrowButton()
    {
        if(nextBtn == null||previousBtn==null) {
            return;
        }
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if (currentPage == 1)
        {
            previousBtn.interactable = false;
        }
        else if(currentPage==maxPage) {
        
        nextBtn.interactable = false;}
    }
}
