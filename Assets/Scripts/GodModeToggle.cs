using TMPro;
using UnityEngine;

public class GodModeToggle : MonoBehaviour
{
    [SerializeField] private GameObject[] healthObjects;
    [SerializeField] private GameObject text;
    private bool _godMod = false;
    
    
    public void ToggleGodMode()
    {
        _godMod = !_godMod;
        text.GetComponent<TMP_Text>().text = _godMod ? "Бессмертие: ВЫКЛ" : "Бессмертие: ВКЛ";
        foreach (var healthObject in healthObjects)
        {
            var heroHealth = healthObject.GetComponent<HeroHealth>();
            if (heroHealth is not null)
            {
                heroHealth.godMode = _godMod;
            }
        }
    }
}