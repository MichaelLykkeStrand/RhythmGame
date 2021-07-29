using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;
    public GameObject comboGroup;
    public Text comboText;
    public Text scoreText;
    public AudioSource hitSound;
    public AudioSource missSound;

    private int comboScore;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        missSound.volume = SettingsController.instance.GetVolume();

        Instance = this;
        comboScore = 0;
        comboText.text = comboScore+"";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        comboScore += 1;
        score += 100 * 1+comboScore/3;
        UpdateUI();
        comboGroup.transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 0.1f);
    }

    public void Hit(int _score)
    {
        score += _score;
        UpdateUI();
        //comboGroup.transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 0.1f);
    }

    private void UpdateUI()
    {
        comboText.text = comboScore + "";
        scoreText.text = "Score: " + score;
    }

    public void Miss()
    {
        comboScore = 0;
        UpdateUI();
        comboGroup.transform.DOShakePosition(0.2f,4f);
        missSound.Play();
    }

    public int GetCombo()
    {
        return this.comboScore;
    }

    public int GetScore()
    {
        return this.score;
    }
}
