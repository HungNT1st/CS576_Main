// using UnityEngine;
// using UnityEngine.UIElements;   
// using System.Collections;

// public class QuestionMenuScript : MonoBehaviour
// {
//     [Header("References")]
//     public FightVillian fightVillian;    
//     public GameObject villain;           
    
//     private UIDocument uiDocument;
    
//     private Label headerLabel;
//     private Button optionA;
//     private Button optionB;
//     private Button optionC;
//     private Button optionD;

//     private int correctAnswer;
//     private bool questionAnswered = false;

//     void Awake()
//     {
//         uiDocument = GetComponent<UIDocument>();
//     }

//     void OnEnable()
//     {
//         var root = uiDocument.rootVisualElement;

//         headerLabel = root.Q<Label>("HeaderLabel");
//         optionA     = root.Q<Button>("OptionA");
//         optionB     = root.Q<Button>("OptionB");
//         optionC     = root.Q<Button>("OptionC");
//         optionD     = root.Q<Button>("OptionD");

//         GenerateQuestion();

//         optionA.clicked += () => OnOptionSelected(optionA);
//         optionB.clicked += () => OnOptionSelected(optionB);
//         optionC.clicked += () => OnOptionSelected(optionC);
//         optionD.clicked += () => OnOptionSelected(optionD);

//         questionAnswered = false;
//     }

//     void OnDisable()
//     {
//         if (optionA != null) optionA.clicked -= () => OnOptionSelected(optionA);
//         if (optionB != null) optionB.clicked -= () => OnOptionSelected(optionB);
//         if (optionC != null) optionC.clicked -= () => OnOptionSelected(optionC);
//         if (optionD != null) optionD.clicked -= () => OnOptionSelected(optionD);
//     }

//     private void GenerateQuestion()
//     {
//         int a = Random.Range(1, 11);
//         int b = Random.Range(1, 11);
//         correctAnswer = a + b;

//         headerLabel.text = $"What is {a} + {b}?";
//         headerLabel.style.color = new StyleColor(Color.white);

//         int[] answers = new int[4];
//         int correctIndex = Random.Range(0, 4);

//         for (int i = 0; i < answers.Length; i++)
//         {
//             if (i == correctIndex)
//             {
//                 answers[i] = correctAnswer;
//             }
//             else
//             {
//                 int distractor;
//                 do
//                 {
//                     distractor = Random.Range(1, 21);  
//                 } while (distractor == correctAnswer);
//                 answers[i] = distractor;
//             }
//         }

//         optionA.text = answers[0].ToString();
//         optionB.text = answers[1].ToString();
//         optionC.text = answers[2].ToString();
//         optionD.text = answers[3].ToString();
//     }

//     private void OnOptionSelected(Button selectedButton)
//     {
//         if (questionAnswered) return;
//         questionAnswered = true;

//         int selectedValue = int.Parse(selectedButton.text);

//         if (selectedValue == correctAnswer)
//         {
//             headerLabel.text = "CORRECT";
//             headerLabel.style.color = new StyleColor(Color.green);

//             if (villain != null)
//             {
//                 Destroy(villain);
//                 Debug.Log("Win a match, remove a villain");
//             }
//             else
//             {
//                 Debug.LogWarning("No villain reference assigned in the QuestionMenuScript!");
//             }
//         }
//         else
//         {
//             headerLabel.text = "WRONG";
//             headerLabel.style.color = new StyleColor(Color.red);

//             Debug.Log("Lose a match, loss health");
//         }

//         StartCoroutine(EndQuestionAfterDelay(2f));
//     }

//     private IEnumerator EndQuestionAfterDelay(float delay)
//     {
//         yield return new WaitForSecondsRealtime(delay);

//         gameObject.SetActive(false);

//         if (fightVillian != null)
//         {
//             fightVillian.CloseQuestionMenu();
//         }
//         else
//         {
//             Debug.LogWarning("FightVillian reference is missing on the QuestionMenuScript!");
//         }
//     }
// }



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

        GenerateQuestion();

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

    private void GenerateQuestion()
    {
        int a = Random.Range(1, 11);
        int b = Random.Range(1, 11);
        correctAnswer = a + b;

        headerLabel.text = $"What is {a} + {b}?";
        headerLabel.style.color = new StyleColor(Color.white);

        int[] answers = new int[4];
        int correctIndex = Random.Range(0, 4);

        for (int i = 0; i < answers.Length; i++)
        {
            if (i == correctIndex)
            {
                answers[i] = correctAnswer;
            }
            else
            {
                int distractor;
                do
                {
                    distractor = Random.Range(1, 21);  
                } while (distractor == correctAnswer);
                answers[i] = distractor;
            }
        }

        optionA.text = answers[0].ToString();
        optionB.text = answers[1].ToString();
        optionC.text = answers[2].ToString();
        optionD.text = answers[3].ToString();
    }

    private void OnOptionSelected(Button selectedButton)
    {
        if (questionAnswered) return;
        questionAnswered = true;

        int selectedValue = int.Parse(selectedButton.text);

        if (selectedValue == correctAnswer)
        {
            headerLabel.text = "CORRECT";
            headerLabel.style.color = new StyleColor(Color.green);

            if (villain != null) Destroy(villain);

            Debug.Log("Win a match, remove a villain");

            if (gameManager != null)
            {
                gameManager.HealWorld(1f);  
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
                gameManager.DamagePlayer(5f);
            }
            else
            {
                Debug.LogWarning("No GameManager reference assigned in QuestionMenuScript!");
            }
        }

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
