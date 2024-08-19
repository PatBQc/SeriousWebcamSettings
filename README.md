# SeriousWebcamSettings

## Intro
Control your Webcam settings (exposure, brightness, zoom, crop, tilt, ...) with one simple Open Source application !

Started with the desire to have a better experience than the Windows build-in little Webcam preference hidden under layers of API call and not readily available in a simple app that could be pinned in the taskbar.

So we passed from this hidden in Windows API

![image](https://user-images.githubusercontent.com/12274241/192283466-fcc6119e-708f-4950-9024-d59b3782f3ba.png)

To this !

![image](https://github.com/user-attachments/assets/66bdb695-dcf1-475b-ba1f-d67806e527b6)



To start, just click **"Choose Device"**.

![image](https://github.com/user-attachments/assets/14a9f258-a371-4e23-933a-4f606cb8ad50)


Then the Web Cam you want to administer

![image](https://github.com/user-attachments/assets/156af4d7-bc7c-4078-836f-bc863330926e)

And there you have it!


Your Webcam image on your left and the settings on your right.

The settings are hardcoded as follow
- Pan
- Tilt
- Roll
- Zoom
- Exposure
- Iris
- Focus
- Brightness
- Contrast
- Hue
- Saturation
- Sharpness
- Gamma
- ColorEnable
- WhiteBalance
- BacklightCompensation
- Gain

For exemple, changing exposure with the scroller, you can see the image got brighter and the **"Current value"** went from **-6** to **-5**

![image](https://github.com/user-attachments/assets/8fdcd34a-a928-4157-a191-33907374b57a)


## Known issues
- The video stream cannot be shared between applications.  This means that you can see it inside the app or give it to Teams, Zoom, ...  I usually start the application that will require the Webcam (OBS, Teams, Zoom, ...) and after that start this **SeriousWebcamSettings**.  Once I change something in this app, you see realtime your modification in the other app.
- If you want to control more than One Webcam at a time, you simply need to start the application twice (if pinned in the Taskbar, Shift+Click the shortcut to launch a new instance).

## Next steps
- [x] Would love to have a simple save / load settings for the various camera.  Lights changes by the time of day and while working from various office, have my presets already available.
- [x] Have the opportunity to force syncing the settings back on regular intervals.  Sometime a webcam settings are reverted to their default value by Windows or the consuming application.  So once you are happy with the settings, forcing to sync back every few seconds would make sure the streams never stays in a faulted state.
- [ ] Implement command line hooks so it can be scripted or pinned in the taskbar for a particular configuration.
- [ ] Have the stream be *shareable* between both this application and the rest of the apps consuming the stream.  Does not seem to be feasible as it is "By Design" in Windows API.
- [ ] Change the Webcam selection input method from the built-in window provided by AForge.Video.DirectShow to have a simple dropdown (ComboBox or something similar) in the MainWindow.
- [ ] Query the available Webcam settings from an API call instead of the hardcoded enum.

## Thanks
Thanks for the great AForge Library and Community still keeping it alive.  Found a simple patch added by the community to access the Webcam underlying settings still floating around.
