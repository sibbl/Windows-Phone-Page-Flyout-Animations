Windows Phone Page Flyout Animations
====================================

This animation helper allows you to easily embed the animations, known from the Windows Phone mail app when submitting, editing or deleting an email.

How to use
----------

The usage is very easy. Just copy the AnimationHelper.cs class file into your project and call it in your own project:

```c#
protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
{
  // you can use AnimationToPlay.SlideDown, SlideUp, SlideLeft and SlideRight
  AnimationHelper.Play(AnimationToPlay.SlideDown, this, LayoutRoot, () =>
  {
      // this code will be run when the animation is complete
      if (NavigationService.CanGoBack) NavigationService.GoBack();
  });
}
```