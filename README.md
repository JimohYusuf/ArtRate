## SETTING UP AND OPERATING THE SMARTWATCH APPLICATION



*   Download and install the application from Tizen app store (Type calmCom in the Tizen app store).

*   Launch the application after installation and one will be required to grant the application permissions to access the device’s sensors (Heart rate sensor in our case). Click the “check mark” sign to grant permission. Note that the app will not work without the permission being granted. 

*   Enter the IP address of the server into the text box that hints “Enter IP address here.”

*   Click the **Connect** button.

*   Then enter the time interval in which you would like to measure and report the heart rate reading to the server(in seconds). Default value is 30 seconds. This means that the watch will report heart rate data to the server every 30 seconds.

*   Click the **Connect** button. 

*   The application will run if the watch has an active internet connection (Wi-Fi, ethernet, or mobile(LTE)) and the server is running.

*   If one gets an error message that says “**Cannot Connect**”, then that indicates that there is a problem with the server or the watch’s internet connection.

*   One might also get an error message that reads “**No Internet Connection**” which, well, indicates that one does not have an active internet connection.

*   One might also want to ensure that they entered in the correct IP address in the address box if one gets a “**Cannot Connect**” error message.

*   If on the other hand, one has an active internet connection, one entered the correct IP address for the server and the server is up and running, then one would be taken to the next screen.

*   Click on the **Start** button to start recording the heart rate data and having it automatically sent to the server.

*   On clicking the Start button, the application would print a toast message: **Heart Rate Monitoring Started**. If one gets an error message, follow the instructions given by the error message. 

*   Wait for about 5-20 seconds for the sensor to initialize.

*   One can leave the app by pressing the home button of the watch and the app will keep running in the background. Make sure NOT to press the back button instead as this would stop the app from running in the background. 

*   One can also always go back to the app and click the **Stop** button to stop the app from recording heart rate data.

*   On stopping the app, the application would print a toast message: **Heart Rate Monitoring Stopped.** 