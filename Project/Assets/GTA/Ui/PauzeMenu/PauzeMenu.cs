using UnityEngine;

public class PauzeMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private MiniMap m_MiniMap;
    [SerializeField]
    private string m_Button;

    private bool m_IsPauzed;

    // Start is called before the first frame update
    void Start()
    {
        UI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_Button))
        {
            if (!m_IsPauzed)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Pause()
    {
        m_MiniMap.HideAll();
        m_IsPauzed = true;
        UI.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPause()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UI.gameObject.SetActive(false);
        m_IsPauzed = false;
        Time.timeScale = 1;
    }
}
