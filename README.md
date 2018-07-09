# Vuforia-Integration-with-Meta-2
A guide on using Vuforia with the Meta 2 headset

Version Information:

Unity: 2017.3.0f3
Vuforia: 7.0.36
Meta SDK: Beta 2.7.0.38

Meta 2 headset is being run in "Direct Mode" (you can change this by opening up "Meta Display Manager" from Start)

Step One:

Delete all gameobjects in the current scene, and save the scene

Step Two:

Import the Meta 2 Unity package from \Meta\Meta SDK2 Beta\Unity

Add the "MetaCameraRig" prefab to the scene

Under "Slam Localizer" DISABLE "Show Calibration UI"
Under "Slam Localizer" ENABLE "Rotation Only Tracking"
This will help us fix the Offset later on

Under "Depth Occlusion" DISABLE "Enable Depth Occlusion"
This will get rid of black spotting caused by the Meta 2 thinking the projection is behind the Image Marker

Step Three:

Enable Vuforia in "Player" settings under "XR Settings" (Edit -> Project -> Player)
Select "Virtual Reality Supported"

Remove all present Virtual Reality SDKs like OpenVR, Occulus, etc... (click on the SDK, and select the minus sign on the bottom-right beside the plus)

Click + and add Vuforia to the Virtual Reality SDKs

Step Four:

Add the "ARCamera" prefab to the scene

Set the "Near Clipping Plane" to "0.015" and the "Far Clipping Plane" to "10"
Set "Target Display" to "Display One"
Set "Target Eye" to "Both"

Step Five:

Open "Vuforia Configuration" and add in your license key
Set "Camera Device Mode" to "MODE_OPTIMIZE_QUALITY"
Set "Device Type" to "Digital Eyewear"
Set "Device Config" to "Vuforia"
DISABLE "video background"
DISABLE "track device pose"
Make sure "Disable Vuforia Play Mode" is NOT checked

Set "Camera Device" to "Meta2 Webcam"

Step Six:

Import and Attach "RGBTransform.cs" to the "ARCamera" prefab
Set "Destination" to "WORLD"
Set "Source" to "RBG"
Set "Angle X" to "-13" (Negative Thirteen)

You will have to play around with x, y, z, Angle X, Angle Y, and Angle Z to get better results

Step Seven:

Create a new Databse on Vuforia's online Target Manager
Upload an image to the Database
Make sure the "Width" you set is the width of the marker in metres. The more accurate the measurement you provide, the better
Download and import unity package of the Database

Add an "ImageTarget" prefab to your scene
Set "Database" to the Database you just created
Set "Image Target" to the image you just uploaded

Go back to "Player" settings and make sure Vuforia is still enabled in XR settings
Go back to the Vuforia Configuration file and enable (and activate) the databse you just uploaded, and disable all the other ones.

Step Eight:

Import and Attach the "DataCollection.cs" script to the "ImageTarget" prefab
Set "pos" to "0.15"
Set "increment" to "0.01"

Step Nine:

Create an Empty Gameobject and call it "Images"
Reset the transform of "Images"
Parent "Images" to "MetaCameraPrefab"

Step Ten:
Create a Cube
Reset its transform
Set the Cube's X and Z scale to be the same as the "ImageTarget"'s scale
Set the Cube's Y scale to be "0.0001"
Make the Cube a child of "Images"

Create a Directional Light
Reset its transform
Parent the Directional Light to the Cube
Set the x-rotation of the Cube to 90, and increase its y-position (you should probably change this later, as this will cause gameobjects to appear "washed out")

Step Eleven:

Import and Attach the "Offset.cs" script to the Cube
Set "Marker" to be the "ImageTarget" prefab by dragging and dropping it on the slot

Step Twelve:

Create an empty .txt file called "data.txt" and place it in Assets\Resources folder in your Unity project

Step Thirteen:

Print and cut out 4 to 6 rulers:
http://www.vendian.org/mncharity/dir3/paper_rulers/

Remember to download the correct PDF for your paper size (I used A4)
Tape the rulers in a straight line on a flat table

Step Fourteen:

Place the Meta 2 so that the front is 9.7cm deep into the ruler.
Use something straight, like a book, and place it at 9.7cm, and then touch the front of the Meta 2 to the book

Step Fifteen:

Glue the marker onto cardboard, or something equally sturdy, so that it can stand straight on the table without your support

Step Sixteen:

Save the scene.
Play the scene.

Place the marker at 15cm.
Look for the "Trackable found" in the Console

Now, left-click.
The Console should say "wrote" and then "0.15"

We are doing this to find a function that will fix the Depth offset caused by Vuforia.
Currently, the ImageTarget has some z-coordinate, but this will most likely not be the true z-coordinate relative to the Meta 2
By placing the Marker at 15cm, and left-clicking, we are saving the z-coordinate that Vuforia reports as the indepedant variable for our funciton, and the true position of the Marker relative to the headset as the dependant variable in "data.txt"

Left-Click 10-20 times over a few seconds, then place the Marker at 16cm.
Now Right-Click. In the console, you should see "increment" and "0.16"

Repeat these steps until you reach about 80-90cm, or more if you want.

Step Seventeen:

Open "data.txt" and press Control-A then Control-V
Go to https://arachnoid.com/polysolve/
Paste the data in the provided textbook
Set the "Degree" to 1
Click "Output Form" so you know which variable is "m" and which is "b" in the linear regression

Step Eighteen:

Open "Offset.cs" and scroll down until you find the CalculateOffsetZ(float MarkerPosZ) function
Replace "m" and "b" with the values you just obtained

Step Nineteen:

That should be it. Play around with "RGBTransform.cs" and repeat Step Fifteen for better results.
