using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace YourProject
{
    public enum AnimationToPlay
    {
        SlideDown,
        SlideUp,
        SlideLeft,
        SlideRight
    }
    public class AnimationHelper
    {
        public static void Play(AnimationToPlay mode, PhoneApplicationPage page, UIElement item, Action callback)
        {
            // init
            var storyboard = new Storyboard { };
            var group = new TransformGroup();
            var translate = new TranslateTransform { X = 0, Y = 0 };
            var scale = new ScaleTransform
            {
                CenterX = item.RenderSize.Width / 2,
                CenterY = item.RenderSize.Height / 2,
                ScaleX = 1,
                ScaleY = 1,
            };
            group.Children.Add(scale);
            group.Children.Add(translate);
            item.RenderTransform = group;

            // scale animation
            var easingInOutFunction = new CubicEase();
            easingInOutFunction.EasingMode = EasingMode.EaseInOut;

            var scaleAnimationX = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(.25),
                To = .5,
                EasingFunction = easingInOutFunction
            };
            storyboard.Children.Add(scaleAnimationX);
            Storyboard.SetTarget(scaleAnimationX, scale);
            Storyboard.SetTargetProperty(scaleAnimationX, new PropertyPath(ScaleTransform.ScaleXProperty));

            var scaleAnimationY = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(.25),
                To = .5,
                EasingFunction = easingInOutFunction
            };
            storyboard.Children.Add(scaleAnimationY);
            Storyboard.SetTarget(scaleAnimationY, scale);
            Storyboard.SetTargetProperty(scaleAnimationY, new PropertyPath(ScaleTransform.ScaleYProperty));

            var easingInFunction = new CubicEase();
            easingInFunction.EasingMode = EasingMode.EaseIn;

            // translate animation
            double translateTo = 0;
            if (mode == AnimationToPlay.SlideDown) translateTo = page.RenderSize.Height;
            else if (mode == AnimationToPlay.SlideUp) translateTo = -1 * page.RenderSize.Height;
            else if (mode == AnimationToPlay.SlideLeft) translateTo = -1 * page.RenderSize.Width;
            else if (mode == AnimationToPlay.SlideRight) translateTo = page.RenderSize.Width;
            object translateParam = (mode == AnimationToPlay.SlideDown || mode == AnimationToPlay.SlideUp) ? TranslateTransform.YProperty : TranslateTransform.XProperty;
            var translateAnimation = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromSeconds(.3),
                Duration = TimeSpan.FromSeconds(.3),
                To = translateTo,
                EasingFunction = easingInFunction
            };
            storyboard.Children.Add(translateAnimation);
            Storyboard.SetTarget(translateAnimation, translate);
            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath(translateParam));

            // callback
            translateAnimation.Completed += (s, arg) => { if (callback != null) callback(); };

            // get the party started
            storyboard.Begin();
        }
    }
}
