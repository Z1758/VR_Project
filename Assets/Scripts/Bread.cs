using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    private static Bread instance;
    Vector3 pos;
    Quaternion quaternion;
    [SerializeField] Rigidbody rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    public static Bread Instance
    {
        get
        {
            return instance;

        }
    }

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(this);
        }
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        pos = transform.position;
        quaternion = transform.rotation;
    }
  




    private void Update()
    {
        if(transform.position.y < 1.3f)
        {
            SetPos();
        }

        if(transform.position.z < 57f)
        {
            rb.velocity = Vector3.zero;
            audioSource.PlayOneShot(clip);
            SpawnBirds.Instance.Spawn(10);
            gameObject.SetActive(false);
        }
    }

    public bool SetBread()
    {
        if (gameObject.activeSelf == true)
            return false;

        gameObject.SetActive(true);
        SetPos();

        return true;
    }

    void SetPos()
    {
        transform.position = pos;
        transform.rotation = quaternion;
    }
}
