using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public Sprite[] stagesSprites;

    private int stagesCount = 4;

    public int finalStageEndurance;

    private int currentTotalEndurance;

    private int endurancePerStage;

    public SpriteRenderer sr;

    public AudioSource growSound;

    private int currentStage;

    public GameObject victory;

    private bool isVictory = false;

    private float victoryCountdown = 3f;

    void Start()
    {
        currentTotalEndurance = 0;
        currentStage = 0;
        endurancePerStage = finalStageEndurance / stagesCount;
    }

    private void Update()
    {
        if (isVictory)
        {
            if (victoryCountdown <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                victoryCountdown -= Time.deltaTime;
            }
        }
    }

    public void addCollectable(BuildableItem item)
    {
        currentTotalEndurance += item.endurance;
        updateCastle();
    }

    private void updateCastle()
    {
        int stage = getStage();

        if (stage > currentStage)
        {
            growSound.Play();
        }

        sr.sprite = stagesSprites[stage];
        currentStage = stage;

        if (stage == stagesCount)
        {
            Instantiate(victory, victory.transform.position, Quaternion.identity);
            isVictory = true;
        }
    }

    public void hitByWave(int strenght)
    {
        int newEndurance = currentTotalEndurance - strenght;

        if (newEndurance < 0)
        {
            newEndurance = 0;
        }

        currentTotalEndurance = newEndurance;

        updateCastle();
    }

    public int getStage()
    {
        int stage = currentTotalEndurance / endurancePerStage;

        if (stage > 4)
        {
            return 4;
        }

        return stage;
    }

    public int getCurrentTotalEndurance()
    {
        return currentTotalEndurance;
    }
}
