using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    [SerializeField, TooltipAttribute("移動先シーン名")]
    private string sceneName;
    [SerializeField, TooltipAttribute("移動先ポイント名")]
    private string posName;



    private void OnTriggerEnter(Collider other)
    {

            
            if (other.CompareTag("Player"))
            {
            GameManager.Instance.SceneChengeManager(sceneName, posName);
            }

    }

     public void SceneChangeVillage()
    {
        SceneManager.LoadScene("真暗");
    }

}
