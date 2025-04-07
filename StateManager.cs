using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
	public WeatherData weatherData;
	public string currentWeather;
	private bool rain;
	private bool snow;
	private bool cloudy;
	private bool sunny;
	public GameObject rainObject;
	public GameObject snowObject;
	public GameObject cloudyObject;
	public GameObject sunnyObject;
    
	void OnEnable()
{
    WeatherScriptableObject.Instance.OnWeatherUpdated += UpdateWeatherVisuals;
}

void OnDisable()
{
    WeatherScriptableObject.Instance.OnWeatherUpdated -= UpdateWeatherVisuals;
}

void UpdateWeatherVisuals(WeatherInfo info)
{
    string icon = info.GetIcon();

    if (icon.StartsWith("09") || icon.StartsWith("10")) {
        SpawnRain();
    } else if (icon.StartsWith("13")) {
        SpawnSnow();
    } else if (icon.StartsWith("02") || icon.StartsWith("03") || icon.StartsWith("04") || icon == "50d") {
        SpawnCloudy();
    } else if (icon.StartsWith("01")) {
        SpawnSunny();
    } else {
        None();
    }
}


	void SpawnRain () {
		rain = true;
		rainObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}


	void SpawnSnow () {
		snow = true;
		snowObject.SetActive (true);
		if (rain) {
			StartCoroutine (DisableRain());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}
	void SpawnCloudy () {
		cloudy = true;
		cloudyObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (rain) {
			StartCoroutine (DisableRain());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		}
	}
	void SpawnSunny () {
		sunny = true;
		sunnyObject.SetActive (true);
		if (snow) {
			StartCoroutine (DisableSnow());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (rain) {
			StartCoroutine (DisableRain());
		}
	}
	void None () {
		if (rain) {
			StartCoroutine (DisableRain());
		} else if (cloudy) {
			StartCoroutine (DisableCloudy());
		} else if (sunny) {
			StartCoroutine (DisableSunny());
		} else if (snow) {
			StartCoroutine (DisableSnow());
		}
	}

	IEnumerator DisableRain() {
		rain = false;
		rainObject.GetComponent<ParticleSystem> ().Stop();
		rainObject.GetComponent<Animator> ().Play ("rain_exit");
		yield return new WaitForSeconds(5);
		rainObject.SetActive (false);
	}

	IEnumerator DisableSnow() {
		snow = false;
		snowObject.GetComponent<ParticleSystem> ().Stop();
		snowObject.GetComponent<Animator> ().Play ("snow_exit");
		yield return new WaitForSeconds(5);
		snowObject.SetActive (false);
	}

	IEnumerator DisableCloudy() {
		cloudy = false;
		cloudyObject.GetComponent<Animator> ().Play ("cloudy_exit");
		yield return new WaitForSeconds(5);
		cloudyObject.SetActive (false);
	}

	IEnumerator DisableSunny() {
		sunny = false;
		sunnyObject.GetComponent<Animator> ().Play ("sunny_exit");
		yield return new WaitForSeconds(5);
		sunnyObject.SetActive (false);
	}
}
