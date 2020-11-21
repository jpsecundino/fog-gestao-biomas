using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlantSO : ScriptableObject
{
    public string plantName;
    public float price;
    public GameObject plantPrefab;
    public Material objHoverFree;
    public Material objHoverOcc;
}
