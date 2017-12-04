using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public delegate void HeartInfoDelegate();

    public static HeartInfoDelegate onHeartGained;
    public static HeartInfoDelegate onHeartLost;
    public static HeartInfoDelegate onHeartReset;

    public static PlayerController instance;

    private static string EnemyTag = "Enemy";

    Rigidbody2D rb;

    public float baseSpeed = 1f;
    public float speedEase = 1f;
    public float recoilEase = 1f;
    public int hearts = 3;

    public GunController initialGun;
    public Image hurtOverlay;
    public GameObject gameOverScreen;

    [HideInInspector]
    public bool dead = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        initialGun.onGunShot += OnGunShot;

        onHeartReset();
        for( int i = 0; i < hearts; ++i) onHeartGained();
        
        LoadingScreen.Close();
	}

    Vector3 recoilSpeed;

    void OnGunShot(float angle, float recoil)
    {
        recoilSpeed = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * -recoil;
    }

    // Update is called once per frame
    void Update () {
        if( ! dead)
        {
            rb.velocity = Vector3.Lerp(
                rb.velocity,
                baseSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0)
                    + recoilSpeed,
                speedEase * Time.deltaTime
                );

            recoilSpeed = recoilSpeed * Mathf.Max(0, 1 - Time.deltaTime / recoilEase);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( !dead && collision.gameObject.tag == EnemyTag)
        {
            hearts -= 1;
            onHeartLost();

            AudioManager.instance.Play("Hurt");

            if( hearts <= 0)
            {
                AudioManager.instance.Play("Game Over");

                dead = true;
                gameOverScreen.SetActive(true);
                rb.mass = 1000000f;
                rb.drag = 1000000f;

            } else
            {

                EnemyController.ForEach(e => e.UniformExplosion(transform.position, 500f));
            }
            hurtOverlay.color = Color.white;
            LeanTween.cancel(hurtOverlay.rectTransform);
            LeanTween.alpha(hurtOverlay.rectTransform, 0, 1.5f)
                .setEaseOutQuad();
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        if (instance == this) instance = null;
    }
}
