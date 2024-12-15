// using UnityEngine;
// using DialogueEditor;

// public class DialoqueTesting : MonoBehaviour
// {
//     void Start()
//     {
//         NPCConversation newConversation = ScriptableObject.CreateInstance<NPCConversation>();
//         Conversation conversation = newConversation.Deserialize();

//         SpeechNode speechNode1 = new SpeechNode
//         {
//             Name = "Character A",
//             Text = "Hello there! How can I help you today?",
//             AutomaticallyAdvance = false
//         };

//         SpeechNode speechNode2 = new SpeechNode
//         {
//             Name = "Character A",
//             Text = "Goodbye! Have a nice day!",
//             AutomaticallyAdvance = false
//         };

//         conversation.Root = speechNode1;

//         OptionNode optionNode1 = new OptionNode
//         {
//             Text = "I need help with a task."
//         };

//         OptionNode optionNode2 = new OptionNode
//         {
//             Text = "I'm just passing by."
//         };

//         speechNode1.Connections.Add(new OptionConnection(optionNode1));
//         speechNode1.Connections.Add(new OptionConnection(optionNode2));
//         optionNode1.Connections.Add(new SpeechConnection(speechNode2));

//         ConversationManager.Instance.StartConversation(newConversation);

//         Debug.Log("Dialogue created and started.");
//     }
// }

