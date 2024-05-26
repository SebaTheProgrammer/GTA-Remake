using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;

    [SerializeField]
    private string[] lines;

    [SerializeField]
    private float textSpeed;

    [SerializeField]
    private MonoBehaviour player;

    [SerializeField]
    private GameObject PlayerUi;

    [SerializeField]
    private string nextButton = "e";

    private int index;

    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(nextButton))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

   public void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());

        PlayerUi.gameObject?.SetActive(false);
        player.enabled = false;
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            player.enabled = true;
            PlayerUi.gameObject?.SetActive(true);
            gameObject?.SetActive(false);
        }
    }

    public void Hide()
    {
        this.gameObject?.SetActive(false);
    }

    public void Show()
    {
        this.gameObject?.SetActive(true);
        index = 0;
    }
}
