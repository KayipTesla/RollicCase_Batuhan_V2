using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject losePanel;
    [SerializeField] public GameObject finishPanel;

    [SerializeField] public GameObject tapToPlayButton;

    [SerializeField] public TextMeshProUGUI finishPanelPointText;
    [SerializeField] public Slider throwForceSlider;

    [SerializeField] GameObject levelSliderAndTexts;
    [SerializeField] public Slider levelSlider;
    [SerializeField] public TextMeshProUGUI levelOnPlayText;
    [SerializeField] public TextMeshProUGUI nextlevelOnPlayText;
    [SerializeField] public TextMeshProUGUI pointTextTotal;

    private void Start()
    {
        UILevelSlider();
        TotalPoint();
    }
    public void UIStartGame()
    {
        tapToPlayButton.SetActive(false);
    }
    public void UILevelSlider()
    {
        levelSliderAndTexts.SetActive(true);
        levelSlider.value = 0; 
        levelSlider.maxValue =GameManager.instance.mapPartCount;
        levelOnPlayText.text = (GameManager.instance.mapsLevel + 1).ToString();
        nextlevelOnPlayText.text = (GameManager.instance.mapsLevel + 2).ToString();
    }
    public void UINextLevel()
    {
        UILevelSlider();
        finishPanel.SetActive(false);
        tapToPlayButton.SetActive(true);
    }
    public void UIGameOver()
    {
        losePanel.SetActive(true);
    }
    public void UIFinish()
    {     
        throwForceSlider.gameObject.SetActive(false);
        finishPanel.SetActive(true);
    }
    public void UITryAgain()
    {
        losePanel.SetActive(false);
        tapToPlayButton.SetActive(true);
    }
    private void Update()
    {
        if (GameManager.instance.gameState ==  GameManager.GameState.StartClimbing)
        {
            throwForceSlider.value = GameManager.instance.throwForce;
        }
        levelSlider.value = GameManager.instance.mapPartIndex;
    }
    private void TotalPoint()
    {
        pointTextTotal.text = GameManager.instance.totalPoint.ToString();
    }
    public void UIFinisPointTransfer()
    {
        pointTextTotal.text = GameManager.instance.totalPoint.ToString();
        finishPanelPointText.text = GameManager.instance.totalPoint.ToString() + " Point";
    }
    public void UIStartClimbing()
    {
        throwForceSlider.gameObject.SetActive(true);
        levelSliderAndTexts.SetActive(false);
    }
}
