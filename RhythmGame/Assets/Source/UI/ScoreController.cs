using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreController : MonoBehaviour
{
    public GameObject comboGroup;
    public Text comboText;
    public AudioClip hitSound;
    public AudioClip missSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }
    }

    public void Hit()
    {
        comboGroup.transform.DOPunchScale(new Vector2(-0.2f, -0.2f), 0.1f);
    }

    public void Miss()
    {
        comboGroup.transform.DOShakePosition(0.2f,4f);
    }
}
