using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveClick : MonoBehaviour
{
    Vector2 dir;
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    
    private void Start() {
        dir = new Vector2(Random.Range(10,-10),Random.Range(10,-10));
    }
    private void Update() {
        transform.Translate(dir*speed*Time.deltaTime);
    }
}
