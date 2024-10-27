using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider backgroundSoundsVolumeSlider;
    public Slider dayTimeSlider;
    public AudioSource musicAS;
    public AudioSource backgroundSoundsAS;
    public GameObject directionalLightObject;
    public GameObject gameMenu;

    private Vector3 originalLightRotation;
    private float lightIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        musicAS = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        backgroundSoundsAS = GameObject.Find("GameManager").GetComponent<AudioSource>();
        musicVolumeSlider.value = 1;
        backgroundSoundsVolumeSlider.value = 1;
        originalLightRotation = directionalLightObject.transform.rotation.eulerAngles;
        lightIntensity = directionalLightObject.GetComponent<Light>().intensity;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        musicAS.volume = musicVolumeSlider.value;
        backgroundSoundsAS.volume = backgroundSoundsVolumeSlider.value;
        UpdateLight();
    }

    private void UpdateLight() {
        Vector3 eulerRotationOfLight = originalLightRotation;
        eulerRotationOfLight.x = UpdateXLightRotation(eulerRotationOfLight.x);
        eulerRotationOfLight.y = UpdateYLightRotation(eulerRotationOfLight.y);
        eulerRotationOfLight.z = UpdateZLightRotation(eulerRotationOfLight.z);

        directionalLightObject.transform.eulerAngles = eulerRotationOfLight;
        UpdateLightIntensity();
    }

    private float UpdateXLightRotation(float x) {
        return x += dayTimeSlider.value * 33.946f;
    }

    private float UpdateYLightRotation(float y) {
        return y += dayTimeSlider.value * 18.553f;
    }

    private float UpdateZLightRotation(float z) {
        return z += dayTimeSlider.value * 93.282f;
    }

    private void UpdateLightIntensity() {
        directionalLightObject.GetComponent<Light>().intensity = lightIntensity + dayTimeSlider.value * 0.8f;
    }

    public void SetActiveObject() {
        gameObject.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void SetInactiveObject() {
        gameObject.SetActive(false);
        gameMenu.SetActive(true);
    }
}
