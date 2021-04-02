using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandBlast : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float speed = 5;
    public float scaleChangeRate = 0;
    public float timer = 0;
    public float duration = 0;
    public float alpha = 1;

    // Update is called once per frame

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < duration)
        {
            //increase scale of object
            scaleChangeRate += speed * Time.deltaTime;
            Vector3 scaleChange = new Vector3(scaleChangeRate, scaleChangeRate, 0);
            transform.localScale += scaleChange;

            //fade out sprite by interoplating alpha value from 0 to 1
            alpha = Mathf.Lerp(1, 0, timer / duration);
            spriteRenderer.color = new Color(spriteRenderer.color.r
                , spriteRenderer.color.g
                , spriteRenderer.color.b
                , alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}