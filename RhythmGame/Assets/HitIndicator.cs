using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitIndicator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        float alpha = 1;
        Tween t = DOTween.To(() => alpha, x => alpha = x, 0, 0.6f);
        t.OnUpdate(() =>
        {
            
            Color currentColor = spriteRenderer.color;
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            spriteRenderer.color = newColor;
        });
        t.OnComplete(() => {
            //TODO callback?
            spriteRenderer.enabled = false;
        });
    }
}
