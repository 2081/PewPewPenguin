using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public static int maxRank;

    public SpriteRenderer mainSprite;

    public float spawnDuration;

    public Sprite[] sprites;

    public Transform[] EnemyPrefabs;

    public int rank = 0;

    public bool fusion = false;

	// Use this for initialization
	void Start () {
        maxRank = EnemyPrefabs.Length - 1;

        if ( fusion )
        {
            Transform t = Instantiate(EnemyPrefabs[Mathf.Min(rank, EnemyPrefabs.Length - 1)]);
            t.position = transform.position;
            t.GetComponent<EnemyController>().fromFusion = true;
            DestroyObject(gameObject);

        } else
        {
            StartCoroutine(SpawnRoutine());
        }
	}
	
	IEnumerator SpawnRoutine()
    {
        int N = sprites.Length;

        for(var i = 0; i < N; ++i)
        {
            mainSprite.sprite = sprites[i];
            LeanTween.scale(mainSprite.gameObject, Vector3.one * 1.25f, 0.375f).setEasePunch();
            yield return new WaitForSeconds(spawnDuration / N);
        }

        Transform t = Instantiate(EnemyPrefabs[Mathf.Min(rank, EnemyPrefabs.Length - 1)]);
        t.position = transform.position;
        EnemyController e = t.GetComponent<EnemyController>();

        LeanTween.scale(mainSprite.gameObject, Vector3.zero, 0.375f).setEaseInQuad().setOnComplete( () => DestroyObject(gameObject) );

    }

    
}
