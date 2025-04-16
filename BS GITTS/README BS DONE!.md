# Tutorial 0-1

## Je ziet hier een kubus en een karakter:

## Als je toets 'E' indrukt, speelt er een animatie af dat het karakter uitvoert.

## Als je de spatiebalk indrukt lanceer je als het ware de kubus omhoog.

![gif](BS%20-%20SampleScene%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.54f1%20_DX11_%202025-03-20%2013-57-12.gif)

### Code: 

![launchCube](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Scripts/LaunchCube.cs)

![eToPray](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Scripts/EToPray.cs)

# Tutorial 2

![gif](BS%20-%20UnityBS%20animation%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.54f1%20_DX11_%202025-03-27%2016-41-55.gif)

## Hierin zie je dat ik door op De pijlen of W en S het karakter kan laten lopen met een aangestuurde animatie.

### Code:

![animation](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Walk%20Anims/animatie.cs)

![MoveBasic](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Walk%20Anims/MoveBasic.cs)

# tutorial 3-4

![gif](BS%20-%20Tutorial2%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.54f1%20_DX11_%202025-03-27%2015-07-32.gif)

## Hierin zie je dat ik de speler kan besturen en munten kan oppakken die bij de score optellen.

### Code:

![collect](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Scripts/Tut%202/Collect.cs)

![disappear](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Scripts/Tut%202/CoinDisappear.cs)

![movement](https://github.com/F4RWAY28/All-Unity/blob/master/BS/Assets/Scripts/Tut%202/Player%20Controls.cs)

# Tutorial 5

![gif](BS%20-%20Tutorial2%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.54f1%20_DX11_%202025-04-02%2012-08-02.gif)

## Hierin zie je dat er een enemy en een speler is.

## Als de speler de Enemy raakt, verdwijnt de speler.

## Als de speler de enemy raakt met een kogel, verdwijnt de enemy.

### Code:

```
Shoot:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    
    public List<GameObject> prefabs;
    public KeyCode shootKey = KeyCode.Mouse0;
    public float delay = 0.5f; 

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            StartCoroutine(AwaitDelay(delay));
        }
    }

    
    public void CallShot()
    {
        StartCoroutine(AwaitDelay(delay));
    }

    private IEnumerator AwaitDelay(float time)
    {
        yield return new WaitForSeconds(time);
        CreateProjectile();
    }
    private void CreateProjectile()
    {
        GameObject ob = Instantiate(prefabs[0]);

        ob.transform.rotation = transform.rotation;
        ob.transform.position = transform.position + transform.forward;

        Destroy(ob, 3f);
    }
}
```


```
KillOnHit:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnHit : MonoBehaviour
{
    public string targetTag;
    public GameObject effect;
    private AudioSource audioSource;

    // Optional: If you want to affect player lives
    private Hearts heartsScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        handleHit(coll.gameObject);
    }

    private void OnTriggerEnter(Collider coll)
    {
        handleHit(coll.gameObject);
    }

    private void handleHit(GameObject other)
    {
        if (other.CompareTag(targetTag))
        {
            // Spawn effect
            if (effect != null)
            {
                GameObject expl = Instantiate(effect, other.transform.position, Quaternion.identity);
                Destroy(expl, 2f);
            }

            // Handle special case for "Player"
            if (targetTag == "Player")
            {
                if (heartsScript == null)
                {
                    heartsScript = FindObjectOfType<Hearts>();
                }

                if (heartsScript != null)
                {
                    heartsScript.Lives--;

                    if (heartsScript.Lives <= 0)
                    {
                        Destroy(other, 0.1f);
                    }
                }
            }
            else
            {
                Destroy(other, 0.1f);
            }

            // Play sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
```


```
PlayerDie:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public string targetTag = "Enemy"; // Default to "Enemy"
    public GameObject effect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ensure the AudioSource is assigned
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag(targetTag)) // Check if the collided object is the enemy
        {
            Debug.Log("Player hit by enemy!");

            // Instantiate the effect at the player's position
            if (effect != null)
            {
                GameObject expl = Instantiate(effect, transform.position, Quaternion.identity);
                Destroy(expl, 2f); // Destroy the effect after 2 seconds
            }

            // Play the sound effect if available
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Destroy the player
            Destroy(gameObject, 0.1f);

            // Destroy the enemy
            Destroy(coll.gameObject, 0.1f);
        }
    }
}
```


```
MoveBullet:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 500f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.transform.forward * speed * Time.fixedDeltaTime;
    }
}
```


![link](Assets/Enemy/Bullet/Bullet.cs)

![link](Assets/Enemy/Shoot/Shoot.cs)

![link](Assets/Enemy/Bullet/Enemy%20Die/Kill%20On%20Hit.cs)

![link](Assets/Enemy/Bullet/Enemy%20Die/Player%20Die.cs)

![link](Assets/Enemy/Bullet/Move%20Bullet.cs)

# Tutorial 6 

![gif](BS%20-%20Tutorial2%20-%20Windows,%20Mac,%20Linux%20-%20Unity%202022.3.54f1%20_DX11_%202025-04-16%2011-15-43.gif)
 
 
 Hierin zie je dat de Enenmy de Player probeert te schieten.

 De Speler kan zelf ook schhieten.

 Als de Kogel de Enemy raakt, zie je dat de Enemy verdwijnt en er een visueel effect afspeelt.

 ### Code:

 ```
 ANIM TRIG:

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrig : MonoBehaviour
{
    public string triggerName;
    public float delay = 0f;
    public float resetTime = 1f;
    public KeyCode triggerKey = KeyCode.None;

    private Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
            Debug.LogWarning("Animator component not found on " + gameObject.name);

        if (audioSource == null)
            Debug.LogWarning("AudioSource component not found on " + gameObject.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            CallTrigger();
        }
    }

    public void CallTrigger()
    {
        StartCoroutine(AwaitDelay(delay));
        StartCoroutine(AwaitReset(resetTime));
    }

    private IEnumerator AwaitDelay(float time)
    {
        yield return new WaitForSeconds(time);

        if (animator != null)
            animator.SetTrigger(triggerName);

        if (audioSource != null)
            audioSource.Play();
    }

    private IEnumerator AwaitReset(float time)
    {
        yield return new WaitForSeconds(time);

        if (animator != null)
            animator.ResetTrigger(triggerName);
    }
}
 ```


 ```
 Hearts:

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    private int lives;
    private int maxLives;
    private Image[] hearts;
    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= maxLives && value >= 0)
            {
                lives = value;
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (i < lives)
                    {
                        hearts[i].enabled = true;
                    }
                    else
                    {
                        hearts[i].enabled = false;
                    }
                }
                if (lives == 0) pauseGame();
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
       Image[] images = GetComponentsInChildren<Image>();
        int count = 0;
        foreach (Image img in images)
        {
            if (img.name == "Heart")
            {
                count++;
            }
        }
        hearts = new Image[count];
        count = 0;
        foreach (Image img in images)
        {
            if (img.name == "Heart")
            {
                hearts[count] = img;
                count++;
            }
        }

        lives = hearts.Length;
        maxLives = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {

       
        
       

        

}
}
 ```


 ```
 EnemyShootingBehaviour:

 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyShootingBehaviour : MonoBehaviour
{
    public Shoot shootScript;
    public AnimTrig triggerAnimationScript;
    public Transform target;
    public float shotRange;
    public bool inCooldown;
    public float coolDownTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        shootScript = GetComponentInChildren<Shoot>();
        triggerAnimationScript = GetComponentInChildren<AnimTrig>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPos);

        Vector3 delta = transform.position - target.position;

        if (delta.magnitude < shotRange && !inCooldown)
        {
            shootScript.CallShot();
            triggerAnimationScript.CallTrigger();
            inCooldown = true;
            StartCoroutine(Cooldown(coolDownTime));
        }
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        inCooldown = false;
    }
}
 ```


 ![link](Assets/New/New%20Enemy/Scripts/AnimTrig.cs)

 ![link](Assets/New/New%20Enemy/Scripts/EnemyShootingBehaviour.cs)

 ![link](Assets/New/New%20Enemy/Scripts/Hearts.cs)

 ### Disclaimer, jammer genoeg werkt het Hearts.cs script nog niet helemaal, maar ik wilde mijn voortgang ervan nog wel even laten zien.

