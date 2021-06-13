using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup DeathPanelUI;
    [SerializeField] private CanvasGroup FinishPanelUI;

    [SerializeField] private float animeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        DeathPanelUI.alpha = 0;
        FinishPanelUI.alpha = 0;
        
        DeathPanelUI.gameObject.SetActive(false);
        FinishPanelUI.gameObject.SetActive(false);
    }

    public void DeathUI(float mode)
    {
        DeathPanelUI.gameObject.SetActive(true);
        Fade(DeathPanelUI, mode);
    }

    public void FinishUI(float mode)
    { 
        FinishPanelUI.gameObject.SetActive(true);
        Fade(FinishPanelUI, mode);
    }

    private void Fade(CanvasGroup panel, float mode)
    {
        StartCoroutine(waitForFade(panel, mode));
    }

    IEnumerator waitForFade(CanvasGroup panel, float mode)
    {
        panel.DOFade(mode, animeTime);
        yield return new WaitForSeconds(animeTime);
    }
}
