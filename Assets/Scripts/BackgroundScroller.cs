using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("Background Scroller")]
    [SerializeField] float backgroundScrollSpeed = 0.1f;

    Material myMaterial;
    Vector2 offset;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(backgroundScrollSpeed, 0f);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
