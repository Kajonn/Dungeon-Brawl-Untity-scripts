using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadowCaster : MonoBehaviour
{
    public float shadowDistance = 0.01f;

    private SpriteRenderer shadowSpriteRenderer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (shadowSpriteRenderer == null)
        {
            player = GameObject.FindWithTag("Player");

            var newGameObject = new GameObject();
            newGameObject.transform.SetParent(transform);
            shadowSpriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            var existingSprite = GetComponent<SpriteRenderer>();
            shadowSpriteRenderer.sprite = existingSprite.sprite;
            shadowSpriteRenderer.sortingLayerName = existingSprite.sortingLayerName;
            shadowSpriteRenderer.sortingLayerID = existingSprite.sortingLayerID;
            var shadowColor = Color.black;
            shadowColor.a = 0.5f;
            shadowSpriteRenderer.color = shadowColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var shadowPos = (transform.position- player.transform.position).normalized * shadowDistance;
        
        shadowSpriteRenderer.transform.localPosition = shadowSpriteRenderer.transform.worldToLocalMatrix * shadowPos;
    }
}
