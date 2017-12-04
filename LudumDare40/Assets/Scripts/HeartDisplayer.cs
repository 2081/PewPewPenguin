using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplayer : MonoBehaviour {

    public RectTransform HeartPrefab;
    public RectTransform heartContainer;

    public float heartAnimDuration = 0.170f;

    private List<RectTransform> hearts = new List<RectTransform>();

	// Use this for initialization
	void Awake () {
        PlayerController.onHeartReset += OnHeartsReset;
        PlayerController.onHeartGained += OnHeartGained;
        PlayerController.onHeartLost += OnHeartLost;
    }
	
	void OnHeartsReset()
    {
        hearts.ForEach(h => RemoveHeart(h));
    }

    float nextStartTime = 0;

    void OnHeartGained()
    {

        RectTransform rt = Instantiate(HeartPrefab);
        hearts.Add(rt);
        rt.SetParent(heartContainer);

        float t = Time.fixedTime;
        float delay = Mathf.Max(0f, nextStartTime - t);

        rt.localScale = Vector3.zero;
        LeanTween.scale(rt, Vector3.one, heartAnimDuration)
            .setEaseInQuad()
            .setEaseOutBounce()
            .setDelay( delay );

        nextStartTime = t + delay + 0.5f * heartAnimDuration;
    }

    void OnHeartLost()
    {
        if(hearts.Count > 0)
            RemoveHeart(hearts[hearts.Count - 1]);
    }

    void RemoveHeart(RectTransform heart)
    {
        hearts.Remove(heart);
        
        if(heart != null)
            LeanTween.scale(heart, Vector3.zero, heartAnimDuration)
                .setEaseOutQuad()
                .setEaseInElastic()
                .setOnComplete(() => Destroy(heart.gameObject));
    }
}
