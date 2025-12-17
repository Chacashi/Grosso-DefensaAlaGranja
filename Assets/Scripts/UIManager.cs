using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CanvasGroup pauseScrean;


    private void OnEnable()
    {
        inputReader.ResumeEvent += SetResume;
        inputReader.PauseEvent += SetPause;
    }

    private void OnDisable()
    {
        inputReader.ResumeEvent -= SetResume;
        inputReader.PauseEvent -= SetPause;
    }


    private void SetPause()
    {
        Time.timeScale = 0.0f;
        pauseScrean.alpha = 1.0f;
        pauseScrean.blocksRaycasts = true;
        pauseScrean.interactable = true;

    }
    private void SetResume()
    {
        Time.timeScale = 1.0f;
        pauseScrean.alpha = 0.0f;
        pauseScrean.blocksRaycasts = false;
        pauseScrean.interactable = false;

    }





}
