using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    [SerializeField, TooltipAttribute("移動先シーン名")]
    private string sceneName;
    private FadeInOut fade;



    private void Update()
    {
        if (fade != null)
        {
            if (fade.CONDITION == FadeInOut.Condition.SCENECHANGE)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
       

    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.GetComponentInChildren<FadeInOut>())
            {
                 fade = other.gameObject.GetComponentInChildren<FadeInOut>();
                fade.CONDITION = FadeInOut.Condition.FADEIN;
        
            }

    }
}
