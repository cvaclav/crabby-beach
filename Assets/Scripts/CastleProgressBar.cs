using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CastleProgressBar : MonoBehaviour
{
    public Sprite[] stagesSprites;

    public Text progressText;

    private Castle castle;

    public SpriteRenderer sr;

    private void Start()
    {
        castle = GameObject.Find("Castle").GetComponent<Castle>();
    }

    void Update()
    {
        progressText.text = castle.getCurrentTotalEndurance() + " / " + castle.finalStageEndurance;
        sr.sprite = stagesSprites[castle.getStage()];
    }
}
