using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float createWaveTime;

    private float waveCountdown;

    public float hitCastleTime;

    private float hittingCastleCountdown;

    private Castle castle;

    public int hitStrength;

    private CollectablesGenerator generator;

    public Text countdownText;

    public AudioSource waveSound;

    void Start()
    {
        waveCountdown = createWaveTime;
        hittingCastleCountdown = waveCountdown + hitCastleTime;
        castle = GameObject.FindGameObjectWithTag("Castle").GetComponent<Castle>();
        generator = GameObject.Find("CollectablesGenerator").GetComponent<CollectablesGenerator>();
    }

    void Update()
    {
        if (waveCountdown <= 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                GameObject wave = transform.GetChild(i).gameObject;
                wave.GetComponent<Animator>().SetTrigger("StartWave");
            }

            waveSound.Play();
            waveCountdown = createWaveTime;
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        if (hittingCastleCountdown <= 0)
        {
            castle.hitByWave(hitStrength);
            generator.generateAfterWave();
            hittingCastleCountdown = waveCountdown + hitCastleTime;
        }
        else
        {
            hittingCastleCountdown -= Time.deltaTime;
        }

        updateText();
    }

    private void updateText()
    {
        countdownText.text = "Wave incoming: " + Mathf.CeilToInt(waveCountdown).ToString() + " s";
    }
}
