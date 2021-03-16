# SST-RASA-TTS

(Speech to text -> ChatBot Neural Network -> Text to speech)

 In this guide, I will walk you through how to create a local server with the ChatBot on it, and then connect to the local server with you Android.

 Rasa-Unity Connection is well documented [here](https://medium.com/analytics-vidhya/integrating-rasa-open-source-chatbot-into-unity-part-1-the-connection-9ba582c804cd)
 
 Android SST/TTS implimentation with native tool, documented well [here](https://github.com/PingAK9/Speech-And-Text-Unity-iOS-Android)
 
# Install and Run

## Install Main Dependencies
1) [Unity](https://store.unity.com/?_ga=2.14326334.805111453.1614788386-1407468680.1601549993#plans-individual)
2) [RASA](https://rasa.com/docs/rasa/installation/)
3) [NGROK](https://ngrok.com/download)

## Clone this project
1) Download the ZIP, or clone

## RASA
1) Train the default model
2) Activate your server locally with the model as followed (in whichever folder your trained model is located)
![2021-03-05 (9)](https://user-images.githubusercontent.com/73109076/110186517-6e970e00-7e0d-11eb-963d-645c896869a0.png)
3) You should see a hello world message on http://localhost:5005/

## NGROK
1) Open NGROK and expose your local server
![2021-03-05 (3)](https://user-images.githubusercontent.com/73109076/110186071-04ca3480-7e0c-11eb-944b-e2392320c5c5.png)
2) Copy the generated URL
![2021-03-06 (2)](https://user-images.githubusercontent.com/73109076/110189176-25978780-7e16-11eb-887d-328adaa08f59.png)
3) You should see the same hello message on this site

### Unity
1) Open Unity Hub. Add this project if it is not there already. Open the project
2) In the "RasaConnect" script, paste the generated URL as followed (append to it "/webhooks/rest/webhook")
![2021-03-06 (4)](https://user-images.githubusercontent.com/73109076/110189177-27f9e180-7e16-11eb-84b1-b3a29b8e4c3c.png)
3) Test that the server is working by running the game in editor mode, and using the Input field functionality

### Android Testing
1) Plug in your andorid with USB
2) In the editor, navigate to "File -> Build Settings"
![2021-03-03 (9)](https://user-images.githubusercontent.com/73109076/109848432-2b3f7280-7c48-11eb-84f5-99737bfe0f24.png)
3) Switch to android mode if it is not already, and press "Build and Run"
![2021-03-03 (8)](https://user-images.githubusercontent.com/73109076/109848272-f8957a00-7c47-11eb-9071-e709db77ab02.png)

 ## Run it on Android and chat to the thing in your device!

### Programming the ChatBot
1) The documentation that describes how to program the chatbot can be found [here](https://rasa.com/docs/rasa/training-data-format).
2) You will need to re-do the above tutorial, and adjust the "RASA" step in order for your programmed bot to work
