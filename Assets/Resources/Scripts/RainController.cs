using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherController : MonoBehaviour
{
    public ParticleSystem rainEffect;
    public ParticleSystem snowEffect;
    public GameObject heatWaveEffect;
    public GameObject coldEffect;
    public float weatherChangeInterval = 20f; // ���� ��ȭ �ֱ� (��)

    private void Start()
    {
        StartCoroutine(ChangeWeatherCycle());
    }

    private IEnumerator ChangeWeatherCycle()
    {
        while (true)
        {
            // ���� ����
            int randomWeather = Random.Range(0, 4); // 0: ��, 1: ��, 2: ����, 3: ����
            switch (randomWeather)
            {
                case 0: // ��
                    rainEffect.Play();
                    snowEffect.Stop();
                    heatWaveEffect.SetActive(false);
                    coldEffect.SetActive(false);
                    break;
                case 1: // ��
                    snowEffect.Play();
                    rainEffect.Stop();
                    heatWaveEffect.SetActive(false);
                    coldEffect.SetActive(false);
                    break;
                case 2: // ����
                    heatWaveEffect.SetActive(true);
                    rainEffect.Stop();
                    snowEffect.Stop();
                    coldEffect.SetActive(false);
                    break;
                case 3: // ����
                    coldEffect.SetActive(true);
                    rainEffect.Stop();
                    snowEffect.Stop();
                    heatWaveEffect.SetActive(false);
                    break;
            }
            yield return new WaitForSeconds(weatherChangeInterval);
        }
    }
}
