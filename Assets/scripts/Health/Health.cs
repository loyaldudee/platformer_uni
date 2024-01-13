using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;
        [SerializeField] public GameObject Checkpoint ; 
    [Header ("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriteRend;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
   
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                foreach(Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    //  void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //         TakeDamage(1);
    // }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invunerability());
        gameObject.transform.position = Checkpoint.transform.position;
        foreach(Behaviour component in components)
                {
                    component.enabled = true;
                }
    }
    
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8,9,true);
        for (int i = 0; i < numberofFlashes; i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberofFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8,9,false);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
