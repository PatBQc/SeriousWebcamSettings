# SeriousWebcamSettings

## Intro
Control your Webcam settings (exposure, brightness, zoom, crop, tilt, ...) with one simple Open Source application !

Started with the desire to have a better experience than the Windows build-in little Webcam preference hidden under layers of API call and not readily available in a simple app that could be pinned in the taskbar.

So we passed from this hidden in Windows API

![image](https://user-images.githubusercontent.com/12274241/192283466-fcc6119e-708f-4950-9024-d59b3782f3ba.png)

To this !

![image](https://user-images.githubusercontent.com/12274241/192281799-5d6be3de-5cdb-4e2c-94ac-ec02ba4d8dad.png)

To start, just click **"Choose Device"**.

Then the Web Cam you want to administer

![image](https://user-images.githubusercontent.com/12274241/192281953-15cc59e6-1d94-4231-88e2-992014ca7870.png)

And there you have it!

![image](https://user-images.githubusercontent.com/12274241/192282110-28f7b888-effc-4fb3-ade0-6317ed64806c.png)

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

![image](https://user-images.githubusercontent.com/12274241/192282343-91c2f2dc-d41f-4ac8-b1a7-8aa0efc20984.png)

## Known issues
- The video stream cannot be shared between applications.  This means that you can see it inside the app or give it to Teams, Zoom, ...  I usually start the application that will require the Webcam (OBS, Teams, Zoom, ...) and after that start this **SeriousWebcamSettings**.  Once I change something in this app, you see realtime your modification in the other app.
- If you want to control more than One Webcam at a time, you simply need to start the application twice (if pinned in the Taskbar, Shift+Click the shortcut to launch a new instance).

## Next steps
- [ ] Would love to have a simple save / load settings for the various camera.  Lights changes by the time of day and while working from various office, have my presets already available.
- [x] Have the opportunity to force syncing the settings back on regular intervals.  Sometime a webcam settings are reverted to their default value by Windows or the consuming application.  So once you are happy with the settings, forcing to sync back every few seconds would make sure the streams never stays in a faulted state.
- [ ] Implement command line hooks so it can be scripted or pinned in the taskbar for a particular configuration.
- [ ] Have the stream be *shareable* between both this application and the rest of the apps consuming the stream.
- [ ] Change the Webcam selection input method from the built-in window provided by AForge.Video.DirectShow to have a simple dropdown (ComboBox or something similar) in the MainWindow.
- [ ] Query the available Webcam settings from an API call instead of the hardcoded enum.

## Thanks
Thanks for the great AForge Library and Community still keeping it alive.  Found a simple patch added by the community to access the Webcam underlying settings still floating around.
