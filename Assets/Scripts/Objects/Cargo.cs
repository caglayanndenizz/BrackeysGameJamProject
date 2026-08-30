using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cargo : MonoBehaviour
{
    public Rigidbody2D cargoRb;
    public bool isDangerous;
    public bool isToxic;
    public bool isCarried = false;

    public float explosionTimer;
    private Player player;
    public TMP_Text timerText;
    public AudioSource cargoAudio;

    public float displayOffset;

    void Start()
    {
        cargoRb = GetComponent<Rigidbody2D>();

        PossibilityOfCargoExplosion();
        player = FindFirstObjectByType<Player>();

        if(timerText == null)
        {
            GameObject timerObj = GameObject.FindGameObjectWithTag("TimerUI");
            if(timerObj != null)
            {
                timerText = timerObj.GetComponent<TMP_Text>();
            }
        }

        if(timerText != null)
        {
            timerText.text = "";
        }

        if(isDangerous)
        {
            explosionTimer = 30f;
            displayOffset = Random.Range(-3f, 3f);
        }
    }

    void Update()
    {
        if(player == null) return;

        if(player.hasHarmlessCargo)
        {
            isToxic = !player.harmlessCargoIsSafe;

            if(isDangerous)
            {
                isDangerous = false;
                if(timerText != null) timerText.text = "";
            }
        }

        ToxicWaste();
        CargoAudio();

        if(!isDangerous) return;
        if(!isCarried) return;

        explosionTimer -= Time.deltaTime;

        float displayedTimer = Mathf.Max(explosionTimer + displayOffset, 0f);
        if(timerText != null) timerText.text = explosionTimer.ToString("F0");

        if(explosionTimer <= 0f)
        {
            Explode();
        }
    }

    public void PossibilityOfCargoExplosion()
    {
        isDangerous = Random.Range(0f, 1f) < 0.4f;
    }

    void Explode()
    {
        AudioManager.instance.Play(AudioManager.instance.explosion);
        player.isToxicActive = false;
        player.TakeDamage(300);
        if(timerText != null) timerText.text = "";
        Destroy(gameObject);
    }

    public void ToxicWaste()
    {
        player.isToxicActive = isToxic && isCarried;

        if(isToxic && isCarried)
        {
            player.TakeDamage(2.5f * Time.deltaTime);
        }
    }

    void CargoAudio()
    {
        if(cargoAudio == null) return;

        if(isDangerous && isCarried)
        {
            if(!cargoAudio.isPlaying || cargoAudio.clip != AudioManager.instance.tickingBomb)
            {
                cargoAudio.clip = AudioManager.instance.tickingBomb;
                cargoAudio.loop = true;
                cargoAudio.Play();
            }
        }
        else if(isToxic && isCarried)
        {
            if(!cargoAudio.isPlaying || cargoAudio.clip != AudioManager.instance.toxic)
            {
                cargoAudio.clip = AudioManager.instance.toxic;
                cargoAudio.loop = true;
                cargoAudio.Play();
            }
        }
        else
        {
            if(cargoAudio.isPlaying) cargoAudio.Stop();
        }
    }

    public void OnPickedUp()
    {
        isCarried = true;
        if(timerText != null && isDangerous) timerText.text = explosionTimer.ToString("F0");
    }
}