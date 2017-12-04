using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour {

    private static LinkedList<EnemyController> instances = new LinkedList<EnemyController>();

    public static void ForEach(System.Action<EnemyController> enemy)
    {
        foreach (var instance in instances) enemy(instance);
    }

    public int rank = 0;

    public float baseSpeed = 1f;
    public float speedEase = 1f;
    public float hitPoints = 3f;
    public int reward = 10;

    public float secondTargetProb = 0.3f;

    Rigidbody2D rb;

    public SpriteRenderer[] flashSprites;
    public float flashDuration = 0.375f;

    static string bulletTag = "Bullet";
    static string enemyTag = "Enemy";
    static Color flashDefaultColor = Color.gray;

    public Transform body;
    public Transform shadow;
    public Transform SpawnPrefab;
    public Transform RewardPrefab;

    [HideInInspector]
    public bool fromFusion = false;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        if( fromFusion)
        {
            GetComponent<Collider2D>().enabled = true;

            foreach (var sr in flashSprites)
            {
                sr.color = flashDefaultColor;
                LeanTween.cancel(sr.gameObject);
                LeanTween.color(sr.gameObject, Color.white, 0.750f).setEasePunch();
            }

            LeanTween.scale(gameObject, transform.localScale * 1.5f, 0.750f).setEasePunch();

        } else
        {
            body.localPosition = Vector3.up * (Camera.main.orthographicSize * 2 + 1);
            body.localScale = Vector3.one * 5;

            shadow.localScale = Vector3.zero;

            LeanTween.moveLocalY(body.gameObject, 0, 0.750f).setEaseInQuad();
            LeanTween.scale(body.gameObject, Vector3.one, 0.750f).setEaseInQuad();
            LeanTween.scale(shadow.gameObject, Vector3.one, 0.750f).setEaseInQuad().setOnComplete(
                () => GetComponent<Collider2D>().enabled = true
            );
        }
        


    }
	
	// Update is called once per frame
	void Update () {
		if( PlayerController.instance != null)
        {
            Vector3 playerPos = PlayerController.instance.transform.position;
            Vector3 targetVelocity = (playerPos - transform.position).normalized * baseSpeed;

            rb.velocity = Vector3.Lerp(
                rb.velocity,
                targetVelocity,
                speedEase * Time.deltaTime
            );
        }
	}

    bool consumed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == bulletTag)
        {
            foreach(var sr in flashSprites)
            {
                sr.color = flashDefaultColor;
                LeanTween.cancel(sr.gameObject);
                LeanTween.color(sr.gameObject, Color.white, flashDuration).setEasePunch();
            }

            hitPoints -= 1f;
            AudioManager.instance.Play("Hit");

            if( hitPoints <= 0.001f )
            {
                EnemyDeathParticleManager.Explode(transform.position);

                AudioManager.instance.Play("Squish");


                int n = Random.Range(0f, 1f) < secondTargetProb ? 2 : 1;

                for(int i = 0; i < n; ++i)
                {
                    Spawn( i == 0 ? rank : 0 );
                }


                Transform t = Instantiate(RewardPrefab);
                t.position = transform.position + Vector3.up * 1 * Mathf.Pow(1.25f, 1 + rank);

                t.GetComponent<RewardController>().SetValue(reward);
                ScoreManager.AddScore(reward);

                Destroy(gameObject);
            }
        } else if (collision.gameObject.tag == enemyTag && !consumed && rank < SpawnController.maxRank )
        {
            EnemyController e = collision.gameObject.GetComponent<EnemyController>();
            if( !e.consumed && e.rank == rank )
            {
                Vector3 pos = (transform.position + e.transform.position) / 2;
                Fuse(pos, true);
                e.Fuse(pos);
            }

        }
    }

    SpawnController Spawn(int rank = 0, bool fusion = false)
    {
        Transform t = Instantiate(SpawnPrefab);
        t.position = transform.position + new Vector3(Random.Range(-0.25f, +0.25f), Random.Range(-0.25f, +0.25f), 0);

        SpawnController sp = t.GetComponent<SpawnController>();
        sp.rank = rank;
        sp.fusion = fusion;
        return sp;
    }

    void Fuse( Vector3 position, bool spawn = false )
    {
        consumed = true;
        GetComponent<Collider2D>().enabled = false;
        LeanTween.move(gameObject, position, 0.375f)
            .setEaseInQuad()
            .setOnComplete(() => {
                if( spawn ) Spawn(rank + 1, true);

                Destroy(gameObject);
            }); ;
    }

    public void UniformExplosion(Vector3 position, float intensity)
    {
        rb.AddForceAtPosition((transform.position - position).normalized * intensity, position, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        instances.AddLast(this);
    }

    private void OnDisable()
    {
        instances.Remove(this);
    }
}
