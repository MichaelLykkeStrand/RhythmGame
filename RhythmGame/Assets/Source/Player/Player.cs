using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float height = 1;

    public float Height { get => height; set => height = value; }
}
