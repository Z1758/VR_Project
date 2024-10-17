using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBird : MonoBehaviour
{
    private static KillBird instance;


    public static KillBird Instance
    {
        get
        {
            return instance;

        }
    }
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource source;
    [SerializeField] LayerMask mask;
    [SerializeField] Text text;
    [SerializeField] GameObject clearText;
    [SerializeField] GameObject effectPrefab;
    RaycastHit hit;

    private void Start()
    {
        SetText();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                if (hit.collider.TryGetComponent(out Boids b))
                {
                    b.TakeDamage();
                  
                }
            }
        }
       
#endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, 100f, mask))
                {
                    if (hit.collider.TryGetComponent(out Boids b))
                    {
                        b.TakeDamage();
                        


                    }
                }

            }

      
        }
    }

    void SetText()
    {
      text.text= SpawnBirds.Instance.spawnCount.ToString();
    }

    public void Kill(Vector3 vec)
    {
        source.PlayOneShot(clip);
        SpawnBirds.Instance.spawnCount--;
        SetText();
        if (SpawnBirds.Instance.spawnCount <= 0)
        {
            clearText.SetActive(true);
        }
        Instantiate(effectPrefab, vec, Quaternion.identity);
    }

    public void Kill()
    {
        source.PlayOneShot(clip);
        SpawnBirds.Instance.spawnCount--;
        SetText();
        if (SpawnBirds.Instance.spawnCount <= 0)
        {
            clearText.SetActive(true);
        }
    }
}
