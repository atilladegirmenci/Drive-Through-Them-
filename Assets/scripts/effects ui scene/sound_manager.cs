using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
    [SerializeField] private AudioSource pistolSound;
    [SerializeField] private AudioSource ARSound;
    [SerializeField] private AudioSource shotgunClick;
    [SerializeField] private AudioSource shotgunShot;
    [SerializeField] private AudioSource shotgunLoadShell;
    [SerializeField] private AudioSource ARLoad;
    [SerializeField] private AudioSource PistolLoad;

    static public sound_manager instance;
    void Start()
    {
        instance = this;
        
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }
    public void PlayPistolSound()
    {
        pistolSound.Play();
    }
    public void PlayARSound()
    {
        ARSound.Play();
    }
    public void PlayShotgunShot()
    {
        shotgunShot.Play();
    }
    public IEnumerator PlayShotgunClick(float delay)
    {
        yield return new WaitForSeconds(delay);
        shotgunClick.Play();
    }
    public void PlayReloadPistol()
    {
        PistolLoad.Play();
    }
    public void PlayShotgunShellLoad()
    {
        shotgunLoadShell.Play();
    }

    public void PlayARLoad()
    {
        ARLoad.Play();
    }
}
