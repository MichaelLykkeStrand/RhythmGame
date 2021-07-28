using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    private static Image image;
    [SerializeField] private static bool isLoading = false;
    private const float minAlpha = 0;
    private const float maxAlpha = 1;
    [SerializeField] private const float animationTime = 0.3f;

    void Awake()
    {
        DontDestroyOnLoad(this);
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    public static void SetLoadImage(Image _image)
    {
        image = _image;
    }

    public static void SetLoading(bool _isLoading)
    {
        Debug.LogWarning("Loading called: Iloading= " + isLoading);
        if (_isLoading && !isLoading)
        {
            image.enabled = true;
            isLoading = true;
            float alpha = 0;
            Tween t = DOTween.To(() => alpha, x => alpha = x, maxAlpha, animationTime);
            t.OnUpdate(() =>
            {
                Color currentColor = image.color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                image.color = newColor;
            });
            t.OnComplete(() => {
                //TODO callback?
            });
        }
        else if(!_isLoading && isLoading)
        {
            float alpha = 1;
            isLoading = false;
            Tween t = DOTween.To(() => alpha, x => alpha = x, minAlpha, animationTime);
            t.OnUpdate(() =>
            {
                Color currentColor = image.color;
                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                image.color = newColor;
            });
            t.OnComplete(() => {
                //TODO callback?
                image.enabled = false;
            });
        }
    }

}
