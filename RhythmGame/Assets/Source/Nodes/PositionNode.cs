using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class PositionNode : MonoBehaviour
{
    [System.Serializable]
    public enum InputEnum
    {
        none,
        up,
        down,
        left,
        right
    };
    [SerializeField] private Sprite perfectSprite;
    [SerializeField] private Sprite okaySprite;
    [SerializeField] private Sprite missSprite;

    [SerializeField] private Plant plant;
    [SerializeField] private PositionNode nextNode;
    [SerializeField] private PositionNode prevNode;
    [SerializeField] private float jumpPower;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private bool isWall = false;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject hitBlock;
    [SerializeField] private GameObject ghostBlock;
    [SerializeField] private float animationTime;
    [SerializeField] private bool isLongNode = false;
    [SerializeField] private GameObject inputIndicator;
    [SerializeField] private HitIndicator hitIndicator;


    private Vector3 ghostBlockSpawnpoint;
    private Vector3 blockSpawnpoint;
    private ParticleSystem particleSystem;
    public float activationTime;
    public float assignedTime;
    [SerializeField] private double visitTime;
    public int index;
    public bool isCheckpoint = false;
    public InputEnum input = InputEnum.none;

    public PositionNode NextNode { get => nextNode; set => nextNode = value; }
    public float JumpPower { get => jumpPower; }
    public float TransitionTime { get => transitionTime; set => transitionTime = value; }
    public bool IsWall { get => isWall; set => isWall = value; }
    public double VisitTime { get => visitTime; set => visitTime = value; }
    public PositionNode PrevNode { get => prevNode; set => prevNode = value; }
    public bool IsLongNode { get => isLongNode; set => isLongNode = value; }

    public string GetInput()
    {
        if (input == InputEnum.none) return "";
        else if (input == InputEnum.up) return "Up";
        else if (input == InputEnum.down) return "Down";
        else if (input == InputEnum.left) return "Left";
        else if (input == InputEnum.right) return "Right";
        return "";
    }

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        block.GetComponent<SpriteRenderer>().enabled = false;
        inputIndicator.GetComponent<SpriteRenderer>().enabled = false;
        ghostBlock.GetComponent<SpriteRenderer>().enabled = true;
        hitBlock.GetComponent<SpriteRenderer>().enabled = false;
        

        blockSpawnpoint = new Vector3(block.transform.position.x, block.transform.position.y);
        ghostBlockSpawnpoint = new Vector3(ghostBlock.transform.position.x, ghostBlock.transform.position.y);

        //Position blocks
        ghostBlock.transform.position = block.transform.position;
        hitBlock.transform.position = ghostBlock.transform.position;
        block.transform.position = ghostBlockSpawnpoint;

        if (input == InputEnum.none) inputIndicator.SetActive(false);
        else if (input == InputEnum.up);
        else if (input == InputEnum.down) inputIndicator.transform.Rotate(0, 0, 180);
        else if (input == InputEnum.left) inputIndicator.transform.Rotate(0, 0, 90);
        else if (input == InputEnum.right) inputIndicator.transform.Rotate(0, 0, 270);

    }

    private bool didFade = false;
    void Update()
    {
        var blockSpriteRenderer = block.GetComponent<SpriteRenderer>();
        if (prevNode == null) {
            block.transform.position = blockSpawnpoint;
            blockSpriteRenderer.enabled = true;
            return;
        }


        //Float in animation
        if (GameController.Instance.GetAudioSourceTime() > activationTime)
        {
            float animTime = assignedTime - activationTime;

            if(didFade == false)
            {
                inputIndicator.GetComponent<SpriteRenderer>().enabled = true;
                block.transform.DOMove(blockSpawnpoint, animTime); //Move into didFade for lerp
                didFade = true;
                float alpha = 0;
                float maxAlpha = 0.85f;
                Tween t = DOTween.To(() => alpha, x => alpha = x, maxAlpha, animTime);
                t.OnUpdate(() =>
                {
                    Color currentColor = blockSpriteRenderer.color;
                    Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
                    blockSpriteRenderer.color = newColor;
                });
                t.OnComplete(()=>
                {
                    inputIndicator.GetComponent<SpriteRenderer>().enabled = false;
                });
            }

            //blockSpriteRenderer.enabled = true;
            
        }

        
        if (GameController.Instance.GetAudioSourceTime() - activationTime > 0)
        {

        }

    }

    //TODO growth animation stuff
    public void Hit(float accuracy)
    {
        particleSystem.Play();
        try
        {
            plant.Grow();
        }
        catch (System.Exception)
        {
        }

        Debug.Log("Acc: "+accuracy);

        if(accuracy <= 0.1 && accuracy >= -0.1)
        {
            hitIndicator.Hit(perfectSprite);
        }
        else
        {
            hitIndicator.Hit(okaySprite);
        }
        
        hitBlock.GetComponent<SpriteRenderer>().enabled = true;
        block.GetComponent<SpriteRenderer>().enabled = false;
        ghostBlock.GetComponent<SpriteRenderer>().enabled = false;
        inputIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Miss()
    {
        hitIndicator.Hit(missSprite);
    }
}
