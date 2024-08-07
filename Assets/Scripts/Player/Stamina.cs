using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina {  get; private set; }
    [SerializeField] private Sprite FullStaminaImage, EmptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina=3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }
    private void Start()
    {

        staminaContainer=GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
    }
    public void RefreshStamina()
    {
        if(CurrentStamina < maxStamina)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }
    public void ResetStamina()
    {
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
        UpdateStaminaImages();
    }
    private IEnumerator RefreshStaminaRountine()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();         
        }
    }
    private void UpdateStaminaImages()
    {
        for(int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite=FullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = EmptyStaminaImage;
            }
        }
        if(CurrentStamina< maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRountine());
        }
    }
}
