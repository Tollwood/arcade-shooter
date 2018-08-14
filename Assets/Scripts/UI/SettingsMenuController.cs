using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour {

    public Toggle campingFeatureToggle;
    CampingFeature campingFeature;
    Color campingFeatureEnabled= new Color(0, 191, 0,77);
    Color campingFeatureDisabled = new Color(147, 15, 15);

    private void Awake()
    {
        campingFeature = FindObjectOfType<CampingFeature>();
        campingFeatureToggle.isOn = PlayerPrefs.GetInt("CampingEnabled", 0) == 0;
    }

    void Start () {
        
	}
	
    public void OnCampingSettingsChanged(){
        campingFeature.enabled = campingFeatureToggle.isOn;
        Image image = campingFeatureToggle.GetComponentInChildren<Image>();
        if(campingFeatureToggle.isOn){
            image.color = Color.green;
            PlayerPrefs.SetInt("CampingEnabled", 0);
        }
        else {
            image.color = Color.red;
            PlayerPrefs.SetInt("CampingEnabled",1);
        }

    }
}
