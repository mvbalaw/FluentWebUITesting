using System;

using FluentWebUITesting.Controls;

namespace FluentWebUITesting.Extensions
{
    public static class ControlWrapperExtensions
    {
        public static void Verify<T>(this T control, params Action<T>[] actions) where T : ControlWrapperBase
        {
            foreach (var action in actions)
            {
                action(control);
            }
        }
    }
}