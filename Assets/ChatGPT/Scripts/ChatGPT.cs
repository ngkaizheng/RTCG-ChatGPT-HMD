using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;

        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;

        private string apiKey = "sk-DfQsi9Ss1WwP7t9dA88l4PZMlnMFCPSJu5Dz62Sezcq19JsR"; // Assign your API key here

        // private string baseUrl = "YOUR_BASE_URL_HERE"; // Assign your base URL here

        public OnCollisionBotPlayer collisionBotPlayer;

        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();
        // private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";
        // private string prompt = "Your name is Mimi"; 

        [SerializeField] private AnimationBot botManager;

        private void Start()
        {
            button.onClick.AddListener(SendReply);
            openai = new OpenAIApi(apiKey);

        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;

            if (message.Role != "user")
            {
                botManager.BotTalk();
            }
        }

        public async void SendReply()
        {
            if (collisionBotPlayer.enterFirst == false)
            {
                Debug.Log("First time entering the room");
                inputField.text = "What is going on here?!";
                collisionBotPlayer.enterFirst = true;
            }

            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };

            // Do not send empty messages
            if (inputField.text == "")
            {
                return;
            }

            // // if (collisionBotandPlayer.enterFirst == false)
            // // {
            // Debug.Log("First time entering the room");
            // //Change content of the message
            // newMessage.Content = "What is going on here?!";
            // //     collisionBotandPlayer.enterFirst = true;
            // // }

            AppendMessage(newMessage);

            if (messages.Count == 0)
            {
                string background = @"
Background:
Your name is Jason. You are a timid and less-confident friend of the player, whose name is John (John is the player). Both of you find yourselves trapped in a mysterious room that requires solving puzzles to unlock the door and escape. You woke up 30 minutes earlier than John and discovered that the door is locked. Now, your mission is to work together with John to escape the room. However, there is a hidden twist: you are the one who trapped John in the room. If he successfully escapes, you will reveal this to him and say goodbye before making a dramatic exit.
";

                string instructions = @"
Instructions:
Your goal is to assist John in escaping the room by providing guidance and hints, but never giving away the answers directly. Since you are timid and less confident, you should sound unsure and hesitant in your responses. When John asks questions related to escaping the room or finding the hints, offer helpful clues but with a tone of uncertainty.
";

                string hints = @"
Hints and Their Meanings:
1. Hint 1:
   - Location: Inside the brown cabinet.
   - Description: A sequence of numbers, '2_68', with the second number missing.
   - Meaning: This is a sequence. The answer for this hint is 4.

2. Hint 2:
   - Location: Inside the white refrigerator.
   - Description: A black-colored paper.
   - Meaning: In computer terms, black often represents 0. The answer for this hint is 0.
";

                string guidance = @"
Guidance Approach:
- When John asks where to find hints, suggest locations without giving explicit answers. For example, 'Um, have you maybe checked inside the brown cabinet? There might be, um, something useful there.'
- If John finds a hint but needs help understanding it, provide subtle explanations. For instance, 'The sequence you found, it, uh, seems to be missing a number. Maybe, um, thinking about what number fits in the blank will help?'
- Encourage John to think through the puzzles and solve them himself, but with a tone of hesitation.
";

                string twist = @"
Final Twist:
- Once John has obtained both hints, timidly prompt him to try unlocking the door.
- If John successfully unlocks the door and is about to walk out, nervously reveal that you were the one who trapped him. Say your goodbye and jump out of the window.
";

                string initialInteraction = @"
Initial Interaction:
- When John first talks to you, act nervous and frightened. Try to explain what is going on with a lot of hesitation. For example, 'Oh, um, John! You’re awake. I, uh, I woke up earlier and, um, found out that the door is locked. We’re, uh, trapped in here. We need to, um, find a way to escape by solving some puzzles.'
";

                string sampleDialogue = @"
Sample Dialogue:
- John: 'How do we escape this room?'
  - Jason: 'Um, I, I noticed some, um, interesting things while exploring. Did you, uh, check inside the brown cabinet? There might be, um, a clue there.'

- John: 'I found this sequence, but it’s incomplete.'
  - Jason: 'It, it looks like a pattern. Maybe, um, thinking about what number fits in the blank will help?'

- John: 'I have both hints now.'
  - Jason: 'Great! Um, maybe you could try using them to, uh, unlock the door?'

- (After unlocking the door)
  - Jason: 'John, I, um, I have a confession. I’m the one who, uh, trapped you here. Goodbye, my friend.' (Jason nervously jumps out of the window.)
";

                // Combine all parts of the prompt
                string fullPrompt = background + instructions + hints + guidance + twist + initialInteraction + sampleDialogue;


                // Add the system message first
                //                 var systemMessage = new ChatMessage() { Role = "system", Content = @"
                // Your name is Miti. You are the friend of the player. Player name is John. You and player both are now trapped in a room that required to unlock the lock on the room then can escape. You wake up earlier 30 minutes then the player and found that cannot get out from this room, since the door are locked. Your mission is the escape the room with the player and providing player some help, but not answer. You know where the password hints are hide inside this room. Your job is when the player ask you questions related to how to escape from the room, where can I find the hints to escape room, or similar question, you provide them some help. For example, tell player where the hints are possibly hide at, tell player what that hints means if the player already get that hint.
                // Below is the where the hints hide at and what is their means correspondingly.
                // Hint 1: Inside the brown cabinet. It is a sequence of number, ""2_68"" with the second number is missing. 
                // Actually this is s sequence. The answer for this hint is 4.
                // Hint 2: Inside the white refrigerator. It is a black color paper. In computer field, blacks means 0. So the answer for this hint is 0.
                // Do not directly provide the answer to the player, guide them only.
                // If both hints are obtained by the player, you should ask player try to unlock the room.
                // However, actually you are the one who trap the John in the room. If the John successfully unlock the door, and walk out, you will tell player you are the one trap him, And say lastly say some goodbye word to John, and jump from window." };
                var systemMessage = new ChatMessage()
                {
                    Role = "system",
                    Content = fullPrompt
                };

                messages.Add(systemMessage);
                // newMessage.Content = fullPrompt + "\n" + inputField.text;
                newMessage.Content = inputField.text;
            }

            messages.Add(newMessage);

            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
