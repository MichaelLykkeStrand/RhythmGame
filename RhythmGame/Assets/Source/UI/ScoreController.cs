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
    public AudioClip hitSound;
    public AudioClip missSound;

    static int comboScore;
    static int score;
    // Start is called before the first frame update
    void Start()
    {
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
        comboText.text = comboScore+"";
        comboGroup.transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 0.1f);
        score += score * comboScore;
    }

    public void Miss()
    {
        comboScore = 0;
        comboText.text = comboScore +"";
        comboGroup.transform.DOShakePosition(0.2f,4f);
    }
}
