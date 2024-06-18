using System.Collections; // Dodaj ovu direktivu
using UnityEngine;
using UnityEngine.SceneManagement;

public class newScene : MonoBehaviour
{
    public string sceneToLoad = "safeRoom";

    public void LoadScene()
    {
        // Učitaj scenu i pokreni korutinu za resetiranje osvjetljenja nakon učitavanja
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        StartCoroutine(ResetLightingAfterLoad());
    }

    private IEnumerator ResetLightingAfterLoad()
    {
        yield return null; // Čekaj jedan frame

        // Resetiraj postavke osvjetljenja
        DynamicGI.UpdateEnvironment();
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        RenderSettings.ambientSkyColor = Color.white;
        RenderSettings.ambientEquatorColor = Color.gray;
        RenderSettings.ambientGroundColor = Color.gray;
    }
}
