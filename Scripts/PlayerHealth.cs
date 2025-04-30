using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    private bool firstHit = true;

    public Image hpBarImage;
    public Sprite fullHP;
    public Sprite fewHP;
    public Sprite halfHP;
    public Sprite lessHP;
    public Sprite lowHP;
    public Sprite NoHP;

    public AudioClip normalHitVoiceLine;
    public GameObject normalHitDialogue;
    public AudioClip lowHPVoiceLine;
    public GameObject lowHPDialogue;

    public AudioClip deathVoiceLine;
    public GameObject deathDialogue;
    private GameObject currentActiveDialogue;
    public AudioClip hitSoundEffect; // Assign in Inspector
    private AudioSource audioSource;
    public GameObject gameOverScreen;
    public GameoverManager gameoverManager;

    public float lowHPThreshold = 0.25f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();

        if (normalHitDialogue != null)
            normalHitDialogue.SetActive(false);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false); // Hide at start
    
}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy!");
            TakeDamage(20);
        }
    }

    public void TakeDamage(int amount)
    {
        
        if (firstHit)
        {
            currentHP -= amount;
            currentHP = Mathf.Max(currentHP, 0); 
            UpdateHPUI();  
            hpBarImage.sprite = fewHP;  
            firstHit = false;  
        }
        else
        {
            currentHP -= amount;
            currentHP = Mathf.Max(currentHP, 0); 
            UpdateHPUI(); 
        }

       
        if (currentHP <= 0)
        {
            Debug.Log("Player is dead.");
            
            if (hitSoundEffect != null)
            {
                audioSource.PlayOneShot(hitSoundEffect);
            }

            if (deathVoiceLine != null)
                audioSource.PlayOneShot(deathVoiceLine);

            if (deathDialogue != null)
            {
                deathDialogue.SetActive(true);
                currentActiveDialogue = deathDialogue;
                Invoke("HideDialogue", 3f);
            }

            if (gameoverManager != null)
            {
                gameoverManager.GameOver(); 
            }
        }
        else if ((float)currentHP / maxHP <= lowHPThreshold)
        {
            
            if (hitSoundEffect != null)
            {
                audioSource.PlayOneShot(hitSoundEffect);
            }
            if (lowHPVoiceLine != null)
                audioSource.PlayOneShot(lowHPVoiceLine);

            if (lowHPDialogue != null)
            {
                lowHPDialogue.SetActive(true);
                currentActiveDialogue = lowHPDialogue;
                Invoke("HideDialogue", 3f);
            }

            hpBarImage.sprite = lowHP; 
        }
        else
        {
            
            if (hitSoundEffect != null)
            {
                audioSource.PlayOneShot(hitSoundEffect);
            }
            if (normalHitVoiceLine != null)
                audioSource.PlayOneShot(normalHitVoiceLine);

            if (normalHitDialogue != null)
            {
                normalHitDialogue.SetActive(true);
                currentActiveDialogue = normalHitDialogue;
                Invoke("HideDialogue", 3f);
            }
        }
    }


    void UpdateHPUI()
    {
        float percent = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            hpBarImage.sprite = NoHP; 
        }
        else if (percent > 0.75f)
        {
            hpBarImage.sprite = fullHP;
        }
        else if (percent > 0.50f)
        {
            hpBarImage.sprite = fewHP;
        }
        else if (percent > 0.25f)
        {
            hpBarImage.sprite = halfHP;
        }
        else if (percent > 0.15f)
        {
            hpBarImage.sprite = lessHP;
        }
        else
        {
            hpBarImage.sprite = lowHP;
        }
    }

    void HideDialogue()
    {

            if (currentActiveDialogue != null)
            {
                currentActiveDialogue.SetActive(false);
                currentActiveDialogue = null; // Clear it after hiding
            }
        }


    }

