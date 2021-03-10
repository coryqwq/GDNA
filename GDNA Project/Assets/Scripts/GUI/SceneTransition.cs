using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SceneTransition : MonoBehaviour
{
    public GameObject mainCamera;

    public Canvas[] menu;
    public GameObject[] firstButton;
    public GameObject[] difficultyButtons;

    public bool titleMenuEnabled = true;
    public bool startMenuEnabled = false;
    public bool difficultyMenuEnabled = false;
    public bool controlsMenuEnabled = false;

    public Animator startMenuAnim;

    public Animator transition;
    public GameObject loadingScreenVideo;
    public float transitionTime = 1f;
    public float fadeTime = 0.25f;
    public void TitleMenu()
    {
        StartCoroutine(TitleMenuTransition());
        menu[0].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void StartMenu()
    {
        //enable start menu and start opening animation
        if(startMenuEnabled == false)
        {
            //set bool of enabled
            titleMenuEnabled = false;
            startMenuEnabled = true;

            //delay disable of title menu canvas, enable start menu canvas
            StartCoroutine(DelayMenuTransition(0, titleMenuEnabled));
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[1]);

            menu[1].gameObject.SetActive(startMenuEnabled);

            //disable block raycasts of title menu, enable block raycasts of start menu
            menu[0].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = titleMenuEnabled;
            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;

            //play start menu opening sound
            gameObject.GetComponent<AudioSource>().Play();
            //start opening animation of start menu
            startMenuAnim.SetBool("Enabled", startMenuEnabled);
        }
        //enable title menu and start closing animation of start menu
        else
        {
            StopAllCoroutines();

            //set bool of enabled
            titleMenuEnabled = true;
            startMenuEnabled = false;

            //disable block raycasts of start menu, enable block raycasts of title menu
            menu[0].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = titleMenuEnabled;
            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;

            //set first selected button
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[0]);

            //enable title menu canvas
            menu[0].gameObject.SetActive(titleMenuEnabled);

            //start closing animation of start menu
            startMenuAnim.SetBool("Enabled", startMenuEnabled);

            //delay disable of start menu canvas
            StartCoroutine(DelayMenuTransition(1, startMenuEnabled));

        }
    }

    IEnumerator DelayMenuTransition(int menuIndex, bool enabled)
    {
        yield return new WaitForSeconds(0.25f);
        menu[menuIndex].gameObject.SetActive(enabled);

    }

    public void DifficultyMenu()
    {
        if (difficultyMenuEnabled == false)
        {

            startMenuEnabled = false;
            difficultyMenuEnabled = true;

            StartCoroutine(DelayMenuTransition(1, startMenuEnabled));

            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[2]);

            menu[2].gameObject.SetActive(difficultyMenuEnabled);

            //highlight the default difficulty button
            difficultyButtons[PlayerPrefs.GetInt("DifficultyIndex")].GetComponent<ButtonFX>().DifficultySelected();

            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;
            menu[2].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = difficultyMenuEnabled;

            startMenuAnim.SetBool("Enabled", startMenuEnabled);
        }
        else
        {
            StopAllCoroutines();

            startMenuEnabled = true;
            difficultyMenuEnabled = false;

            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;
            menu[2].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = difficultyMenuEnabled;

            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[1]);

            menu[1].gameObject.SetActive(startMenuEnabled);

            startMenuAnim.SetBool("Enabled", startMenuEnabled);

            StartCoroutine(DelayMenuTransition(2, difficultyMenuEnabled));

        }
    }

    public void ControlsMenu()
    {
        if (controlsMenuEnabled == false)
        {
            startMenuEnabled = false;
            controlsMenuEnabled = true;

            StartCoroutine(DelayMenuTransition(1, startMenuEnabled));

            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[3]);

            menu[3].gameObject.SetActive(controlsMenuEnabled);

            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;
            menu[3].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = controlsMenuEnabled;

            startMenuAnim.SetBool("Enabled", startMenuEnabled);
        }
        else
        {
            StopAllCoroutines();

            startMenuEnabled = true;
            controlsMenuEnabled = false;

            menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = startMenuEnabled;
            menu[3].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = controlsMenuEnabled;

            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(firstButton[1]);

            menu[1].gameObject.SetActive(startMenuEnabled);

            startMenuAnim.SetBool("Enabled", startMenuEnabled);

            StartCoroutine(DelayMenuTransition(3, controlsMenuEnabled));

        }
    }

    public void StartLevel()
    {
        StartCoroutine(DelayMenuTransition(1, false));
        StartCoroutine(StartLevelTransition());
        menu[1].gameObject.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        StartCoroutine(QuitTransition());
    }

    IEnumerator TitleMenuTransition()
    {
        mainCamera.GetComponent<FadeOutBGM>().FadeOut();

        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeTime);
        loadingScreenVideo.SetActive(true);
        yield return new WaitForSeconds(fadeTime - (fadeTime * 0.5f));
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeTime * 2);

        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator StartLevelTransition()
    {
        mainCamera.GetComponent<FadeOutBGM>().FadeOut();

        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeTime);
        loadingScreenVideo.SetActive(true);
        yield return new WaitForSeconds(fadeTime - (fadeTime * 0.5f));
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(fadeTime * 2);

        SceneManager.LoadScene("LevelScene");
    }

    IEnumerator QuitTransition()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
