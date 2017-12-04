using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {


    public float duration = 3f;
    public AnimationCurve incrementCurve;

    public Text[] scoreTexts;
    public Text[] timeTexts;

    int seconds;
    int score;

    // Use this for initialization
    void Start () {
        score = ScoreManager.GetScore();
        seconds = ScoreManager.GetSecondsSinceStart();
        StartCoroutine(TextsRoutine());
	}

    IEnumerator TextsRoutine()
    {
        float dt = 0;
        do
        {
            dt += Time.deltaTime;
            float dx = Mathf.Min(1f, incrementCurve.Evaluate(dt / duration));
            int sc = Mathf.FloorToInt(dx * score);
            int secs = Mathf.FloorToInt(dx * seconds);

            foreach (var txt in scoreTexts ) txt.text = Utils.ToFormattedInteger(sc) + " x";
            foreach (var txt in timeTexts) txt.text = Utils.ToFormattedTime(secs);

            if( dt == 0)
            {
                yield return new WaitForSeconds(1.25f);
            } else
            {
                yield return new WaitForEndOfFrame();
            }
        } while (dt < duration) ;

    }
}
