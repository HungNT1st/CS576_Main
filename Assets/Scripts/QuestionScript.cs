using UnityEngine;
using UnityEngine.UIElements;   
using System.Collections;

public class QuestionMenuScript : MonoBehaviour
{
    [Header("References")]
    public FightVillian fightVillian;       
    public GameManager gameManager;         
    public GameObject villain;             

    private UIDocument uiDocument;

    private Label headerLabel;
    private Button optionA;
    private Button optionB;
    private Button optionC;
    private Button optionD;

    private int correctAnswer;
    private bool questionAnswered = false;
    private string prompt = "Imagine you are an environmentalist. Create a short question maximum 30 words for elementary students about protecting the environment. It is a multiple-choice question with 4 options to answer. In the JSON you give back to me, you must indicate which answer is correct, which answer is wrong. The Json schema is as follow: There is a question field, containing the question. Follow it is a options list contain 4 options. Each of the option has answer field and isCorrect field. ";

    private string correctAnswerText;
    private OpenAIChatGPT chatGPT = new OpenAIChatGPT();

    void Start()
    {
        chatGPT = GetComponent<OpenAIChatGPT>();

        // Time how long it takes to get a response from ChatGPT
        // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        // stopwatch.Start();
        // StartCoroutine(chatGPT.GetChatGPTResponse(prompt, (response) => {
        //     stopwatch.Stop();
        //     Debug.Log($"ChatGPT Response took: {stopwatch.ElapsedMilliseconds}ms");
        //     OnResponseReceived(response);
        // }));
    }
    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    void OnEnable()
    {
        var root = uiDocument.rootVisualElement;

        headerLabel = root.Q<Label>("HeaderLabel");
        optionA     = root.Q<Button>("OptionA");
        optionB     = root.Q<Button>("OptionB");
        optionC     = root.Q<Button>("OptionC");
        optionD     = root.Q<Button>("OptionD");

        // GenerateQuestion();
        optionA.style.display = DisplayStyle.None;
        optionB.style.display = DisplayStyle.None;
        optionC.style.display = DisplayStyle.None;
        optionD.style.display = DisplayStyle.None;

        // headerLabel.text = "Loading question...";
        StartCoroutine(chatGPT.GetChatGPTResponse(prompt, (response) => {
            OnResponseReceived(response);

            optionA.style.display = DisplayStyle.Flex;
            optionB.style.display = DisplayStyle.Flex;
            optionC.style.display = DisplayStyle.Flex;
            optionD.style.display = DisplayStyle.Flex;
        }));

        optionA.clicked += () => OnOptionSelected(optionA);
        optionB.clicked += () => OnOptionSelected(optionB);
        optionC.clicked += () => OnOptionSelected(optionC);
        optionD.clicked += () => OnOptionSelected(optionD);

        questionAnswered = false;
    }

    void OnDisable()
    {
        if (optionA != null) optionA.clicked -= () => OnOptionSelected(optionA);
        if (optionB != null) optionB.clicked -= () => OnOptionSelected(optionB);
        if (optionC != null) optionC.clicked -= () => OnOptionSelected(optionC);
        if (optionD != null) optionD.clicked -= () => OnOptionSelected(optionD);
    }

    [System.Serializable]
    public class ResponseData
    {
        public string question;
        public Option[] options;
    }
    [System.Serializable]
    public class Option
    {
        public string answer;
        public bool isCorrect;
    }

    void OnResponseReceived(string response)
    {
        Debug.Log("ChatGPT Response: " + response);
        ResponseData questionData = JsonUtility.FromJson<ResponseData>(response);

        headerLabel.text = questionData.question;
        headerLabel.style.color = new StyleColor(Color.white);

        optionA.text = questionData.options[0].answer;
        optionB.text = questionData.options[1].answer;
        optionC.text = questionData.options[2].answer;
        optionD.text = questionData.options[3].answer;

        for (int i = 0; i < questionData.options.Length; i++)
        {
            if (questionData.options[i].isCorrect)
            {
                correctAnswerText = questionData.options[i].answer;
                break;
            }
        }
    }

    // private void GenerateQuestion()
    // {
    //     int a = Random.Range(1, 11);
    //     int b = Random.Range(1, 11);
    //     correctAnswer = a + b;

    //     headerLabel.text = $"What is {a} + {b}?";
    //     headerLabel.style.color = new StyleColor(Color.white);

    //     int[] answers = new int[4];
    //     int correctIndex = Random.Range(0, 4);

    //     for (int i = 0; i < answers.Length; i++)
    //     {
    //         if (i == correctIndex)
    //         {
    //             answers[i] = correctAnswer;
    //         }
    //         else
    //         {
    //             int distractor;
    //             do
    //             {
    //                 distractor = Random.Range(1, 21);  
    //             } while (distractor == correctAnswer);
    //             answers[i] = distractor;
    //         }
    //     }

    //     optionA.text = answers[0].ToString();
    //     optionB.text = answers[1].ToString();
    //     optionC.text = answers[2].ToString();
    //     optionD.text = answers[3].ToString();
    // }

    private void OnOptionSelected(Button selectedButton)
    {
        if (questionAnswered) return;
        questionAnswered = true;

        // int selectedValue = int.Parse(selectedButton.text);
        Debug.Log(selectedButton.text);
        if (selectedButton.text == correctAnswerText)
        {
            headerLabel.text = "CORRECT";
            headerLabel.style.color = new StyleColor(Color.green);

            if (villain != null)
            {
                villain.GetComponent<VillainBehavior>().TakeAllDamage();
                Destroy(villain);
            }

            Debug.Log("Win a match, remove a villain");

            if (gameManager != null)
            {
                gameManager.HealWorld(3f);
            }
            else
            {
                Debug.LogWarning("No GameManager reference assigned in QuestionMenuScript!");
            }
        }
        else
        {
            headerLabel.text = "WRONG";
            headerLabel.style.color = new StyleColor(Color.red);

            Debug.Log("Lose a match, loss health");

            if (gameManager != null)
            {
                gameManager.DamagePlayer(10f);
            }
            else
            {
                Debug.LogWarning("No GameManager reference assigned in QuestionMenuScript!");
            }
        }
        // if (selectedValue == correctAnswer)
        // {
        //     headerLabel.text = "CORRECT";
        //     headerLabel.style.color = new StyleColor(Color.green);

        //     if (villain != null) {
        //         villain.GetComponent<VillainBehavior>().TakeAllDamage();
        //         Destroy(villain);
        //     }

        //     Debug.Log("Win a match, remove a villain");

        //     if (gameManager != null)
        //     {
        //         gameManager.HealWorld(3f);  
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No GameManager reference assigned in QuestionMenuScript!");
        //     }
        // }
        // else
        // {
        //     headerLabel.text = "WRONG";
        //     headerLabel.style.color = new StyleColor(Color.red);

        //     Debug.Log("Lose a match, loss health");

        //     if (gameManager != null)
        //     {
        //         gameManager.DamagePlayer(10f);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No GameManager reference assigned in QuestionMenuScript!");
        //     }
        // }

        StartCoroutine(EndQuestionAfterDelay(2f));
    }

    private IEnumerator EndQuestionAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        gameObject.SetActive(false);

        if (fightVillian != null)
        {
            fightVillian.CloseQuestionMenu();
        }
        else
        {
            Debug.LogWarning("FightVillian reference is missing on the QuestionMenuScript!");
        }
    }
}
