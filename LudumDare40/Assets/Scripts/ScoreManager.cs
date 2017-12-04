using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static void AddScore(int value)
    {
        if( instance != null)
        {
            instance.targetScore += value;
            instance.UpdateText();
        }
    }

    public static void ResetScore()
    {
        if (instance != null)
        {
            instance.targetScore = 0;
            instance.UpdateText();
        }
    }

    public static int GetScore()
    {
        return instance == null ? 0 : instance.targetScore;
    }

    public static int GetSecondsSinceStart()
    {
        return instance == null ? 0 : Mathf.FloorToInt(Time.fixedTime - instance.startTime);
    }
    
    public Text scoreText;
    public Text timeText;

    public float score = 0;
    private int targetScore = 0;

    private float startTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startTime = Time.fixedTime;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = Utils.ToFormattedInteger(Mathf.FloorToInt(score)) + " x";
    }

    private void UpdateTime()
    {
        timeText.text = Utils.ToFormattedTime(GetSecondsSinceStart());
    }

    private void Update()
    {
        if( (float)targetScore != score)
        {
            float ds = targetScore - score;

            if( Mathf.Abs(ds) < 0.5f)
            {
                score = targetScore;
            } else
            {
                score = score + Mathf.Min(ds, 5 * ds * Time.deltaTime);
            }

            
            UpdateText();
        }
        UpdateTime();
    }

}
