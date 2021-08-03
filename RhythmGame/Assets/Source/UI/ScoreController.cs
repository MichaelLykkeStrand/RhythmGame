using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    public const int PERFECT = 200;
    public const int OKAY = 100;
    public static ScoreController Instance;
    public GameObject comboGroup;
    public Text comboText;
    public Text scoreText;

    //TODO Move this to own class
    public AudioSource hitSound;
    public AudioSource missSound;

    private int comboScore;
    private int score = 0;

    public int Score { get => score; set => score = value; }

    // Start is called before the first frame update
    void Start()
    {
        GameController.EventBus.Subscribe<NodeHitEvent>(OnNodeHit);
        GameController.EventBus.Subscribe<NodeMissEvent>(OnNodeMiss);
        missSound.volume = SettingsController.instance.GetVolume();

        Instance = this;
        comboScore = 0;
        comboText.text = comboScore+"";
    }

    void OnNodeHit(NodeHitEvent hitEvent)
    {
        if (hitEvent.accuracy <= 0.1 && hitEvent.accuracy >= -0.1)
        {
            Add(ScoreController.PERFECT, false);
        }
        else
        {
            Add(ScoreController.OKAY, false);
        }
    }

    void OnNodeMiss(NodeMissEvent nodeMissEvent)
    {
        BreakCombo();
    }


    public void Add(int _score, bool silent)
    {
        if(silent == false)
        {
            comboScore += 1;
            Score += _score * 1 + comboScore / 3;
            UpdateUI();
            comboGroup.transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 0.1f);
        }
        else
        {
            Add(_score);
        }
    }

    public void Add(int _score)
    {
        Score += _score;
        UpdateUI();
    }

    private void UpdateUI()
    {
        comboText.text = comboScore + "";
        scoreText.text = "Score: " + Score;
    }

    public void BreakCombo()
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
        return this.Score;
    }
}
