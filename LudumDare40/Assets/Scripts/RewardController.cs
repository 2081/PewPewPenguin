using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardController : MonoBehaviour {

    public SpriteRenderer[] sprites;
    public TextMeshPro[] texts;

    public TextMeshPro valueTMP;

	// Use this for initialization
	void Start () {

        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, Vector3.one, 0.375f)
            .setEaseInOutQuad();

        LeanTween.moveLocalY(gameObject, transform.position.y + 5, 1.5f)
            .setEaseInQuad()
            .setDelay(0.5f);

        foreach (var sr in sprites)
        {
            LeanTween.color(gameObject, new Color(1,1,1,0), 1.5f)
            .setEaseInQuad()
            .setDelay(0.5f);
        }

        foreach (var t in texts)
        {
            LeanTween.value(t.gameObject, t.color, new Color(1, 1, 1, 0), 1.5f)
                .setOnUpdateColor(color => t.color = color)
                .setEaseInQuad()
                .setDelay(0.5f);
        }

        LeanTween.delayedCall(2f, () => Destroy(gameObject));


    }

    public void SetValue(int value)
    {
        valueTMP.text = value.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
