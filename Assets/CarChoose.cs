using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChoose : MonoBehaviour
{
    public SpriteRenderer car;
    public Sprite carSprite1;
    public Sprite carSprite2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            car.sprite = carSprite1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            car.sprite = carSprite2;
        }
    }
}
