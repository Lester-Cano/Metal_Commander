using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Color tmpColor = new Color();
    Unit unit;


    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        unit = GetComponentInParent<Unit>();
        tmpColor = spriteRenderer.color;

    }

    // Update is called once per frame
    void Update()
    {

        if (unit.hasMoved)
        {
            spriteRenderer.color = tmpColor;
        }
       




    }


    void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.tag == "Enemy" && Input.GetKeyDown(KeyCode.Space))
        {
                unit.Attack(collision.gameObject.GetComponent<Unit>());
            
        }

    }

}
