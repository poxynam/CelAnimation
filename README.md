# CelAnimation

This is my custom "animation system" for Unity. It only includes features that I need in the games I'm developing, and might still grow from here.

A LOT of the code was "borrowed" from aarthificial's [Reanimator](https://github.com/aarthificial/reanimation). Licensing information in Third Party Notices.md. (If I did this completely wrong - which I most likely did -, reach out thank you.) Also followed [TheKiwiCoder's](https://www.youtube.com/@TheKiwiCoder) tutorials to create the animation controller UI.

You can install this as a [Unity package](https://docs.unity3d.com/Manual/upm-ui-giturl.html) using the url:

```
https://github.com/poxynam/CelAnimation.git
```


### There is no documentation, but here is a rundown:

Create animations with the correct sprites.

Create a cel animation controller and put your animations into the list on the object.

You can open the controller UI either at the top of the Unity window or by double clicking the controller. In the UI you can see all your animations in one place and edit them.

Now put a cel animator on the object you want to animate, set the controller and yay.

Play animations with the CelAnimator.Play() method, with the parameter being the exact name of the animation.

There are also events and such but who cares honestly.
