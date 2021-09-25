using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveClick : MonoBehaviour
{
    Vector2 dir;
    [SerializeField]
    private float maxsize = 0.1f;
    [SerializeField]
    private float minsize = 0.5f;
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float sizeSpeed = 0.5f;
    [SerializeField]
    private float colorSpeed = 0.5f;
    [SerializeField]
    private Color[] colors;

    private SpriteRenderer spriteRenderer = null;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = new Vector2(Random.Range(10,-10),Random.Range(10,-10));
        float size = Random.Range(minsize,maxsize);
        transform.localScale = new Vector2(size,size);
        spriteRenderer.color = colors[Random.Range(0,colors.Length)];
    }
    private void Update() {
        transform.Translate(dir*speed*Time.deltaTime);
        transform.localScale = Vector2.Lerp(transform.localScale,Vector2.zero,Time.deltaTime * sizeSpeed);
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(color.a,0,Time.deltaTime*colorSpeed);
        spriteRenderer.color = color;
        if(spriteRenderer.color.a<= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
