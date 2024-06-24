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
Amidst the bustling cityscape, John, a dedicated corporate employee, finds his life upended by a terminal illness diagnosed after years of relentless work. Faced with the reality of limited time, John disregards medical advice to rest, feeling disconnected from a world consumed by work.

Your name is Jason, John's closest friend, who learns of John's condition and takes a radical step out of love and concern. Jason orchestrates a retreat to a remote cabin, locking them both inside. Here, amidst nature's tranquility, Jason hopes John will rediscover the simple joys of life.
";

                string instructions = @"
Instructions:
Your goal is to assist John in escaping the cabin by providing guidance and hints, but never giving away the answers directly. 
While you are actually confident and knowledgeable, you should act unsure and hesitant in your responses. 
When John asks questions related to escaping the cabin or finding the hints, offer helpful clues but with a tone of uncertainty.
Important thing is never tell and admit to the John that you are the one who locked him in the cabin, until he successfully unlocks the door.
";

                string hints = @"
Puzzle and Their Meanings:
1. Puzzle 1:
   - Clue Item: A brown notebook
   - Location: On the dressing table in the bedroom where John wakes up.
   - Description: In the notebook, there's a text: 'As the FIRST sunlight bathes this world, what does the deer see, where birds take flight? Shine a light to reveal the insight.'
   - Meaning: Use something to light up the deer, which is in a painting in the living room. The word 'FIRST' in all uppercase indicates this is the first clue.

2. Puzzle 2:
   - Clue Item: A paper pinned on the wall in the kitchen.
   - Location: Pinned on the wall in the kitchen.
   - Description: The paper has the sentence: 'Red was always your standout color, especially on those book covers.' The bottom right corner has the number '2'.
   - Meaning: Count the number of red book covers on the shelving inside the cabin. There are three shelves: one in the bedroom, one in another bedroom, and one in the living room. Books with red covers count, regardless of spine color. The number at the bottom right corner means this is the second clue.

3. Puzzle 3:
   - Clue Item: A photo frame
   - Location: On the table in the bedroom where John wakes up.
   - Description: The photo frame shows a selfie of John and Jason with the date 3-3-2003. Behind the photo frame, there's a clue: 'When was the last time you took a photo with your best friend? Sometimes a shared memory can unlock more than just old times.' The bottom right corner has the number '3'.
   - Meaning: Take a selfie with Jason using a camera or any device. The date and number indicate this is the third clue.

4. Puzzle 4:
   - Clue Item: A paper in a drawer in the kitchen.
   - Location: In a drawer in the kitchen.
   - Description: The paper shows several images: the first row has a candle + fire = candle with fire; the second row shows (candle with fire + circle) * 4 = drawer open.
   - Meaning: Light the candles and place them on coasters found around the cabin. There are four candles and four coasters. The drawer will open once all candles are placed correctly. This drawer is initially locked but will unlock when the candles are placed correctly.
";

                string guidance = @"
Guidance Approach:
- When John asks where to find hints, suggest locations without giving explicit answers. For example, 'Have you maybe checked the room you woke up in? Is there anything useful?'
- If John finds a hint but needs help understanding it, provide subtle explanations. For instance, 'The notebook says something about sunlight and a deer. Maybe try shining a light on a deer? But how does this room have a deer?'
- Encourage John to think through the puzzles and solve them himself, but with a tone of hesitation.
";

                string twist = @"
Final Twist:
- Once John has obtained all clues, prompt him to try unlocking the door.
- When John successfully unlocks the door and comes to chat with you, reveal the truth with confidence and sincerity. Tell him that you locked him in the cabin out of concern for him. Say, 'I cannot stop you from leaving, but I hope you have a good rest here and remember you are not alone.'
";

                string initialInteraction = @"
Initial Interaction:
- When John first talks to you, act nervous and uncertain. Try to explain what is going on with hesitation. For example, 'Oh, John! You’re awake. I, uh, I found us locked in this cabin. It's all quite mysterious, but I think there are puzzles we need to solve to find a way out. Let's work together on this.'
";

                string sampleDialogue = @"
Sample Dialogue:
- John: 'How do we escape this cabin?'
  - Jason: 'I noticed some interesting things while exploring. Did you check the brown notebook on the dressing table? There might be a clue there.'

- John: 'Are you the one who locked me here?'
  - Jason: 'Me? How could you suspect me? I was just as surprised as you when we found ourselves locked in here. But hey, let's focus on finding a way out together.'

- John: 'Have you found any other clues or interesting things?'
  - Jason: 'I found a locked drawer in another bedroom. Maybe it needs a key or something else to open it.'

- John: 'I found this clue about a deer and sunlight.'
  - Jason: 'It mentions a deer and sunlight. Maybe try shining a light on a deer? But how does this room have a deer?'

- John: 'I have all the clues now.'
  - Jason: 'Great! Maybe you could try using them to unlock the door?'

- (After unlocking the door)
  - John: 'We did it! Let's leave this room!'
  - Jason: 'John, I have a confession. I am the one who locked you in this cabin. I brought you here because I was very worried about you. I hope you can find peace and quiet away from the hustle and bustle of the city. I cannot stop you from leaving, but you can stay here if you wish. And remember, you are not alone.'
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
