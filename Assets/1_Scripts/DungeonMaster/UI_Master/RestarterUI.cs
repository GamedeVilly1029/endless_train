using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestarterUI : MonoBehaviour
{
    [SerializeField] private Animator _blackScreenOverlay;
    [SerializeField] private UIMaster _uIMaster;

    WaitForSeconds _keepDarkenAnimation = new(1f);
    private IEnumerator DarkenScreen()
    {
        _blackScreenOverlay.Play("Darken");
        yield return null;

        yield return new WaitForSeconds(_blackScreenOverlay.GetCurrentAnimatorStateInfo(0).length);
    }

    public IEnumerator LoadMainMenu()
    {
        yield return DarkenScreen();
        yield return _keepDarkenAnimation;
        SceneManager.LoadScene(1);
    }
}