using System;
using System.Runtime.CompilerServices;
using CoreGraphics;

namespace EOS.UI.iOS.Extensions
{
    public static class CGSizeExtension
    {
        /// <summary>
        /// Describes ellipse around size rectangle and return ellipse width
        /// </summary>
        /// <returns>The ellipse width described around rectangle.</returns>
        /// <param name="size">Rectangle size.</param>
        public static double GetEllipseWidth(this CGSize size)
        {
            //Assume that textSize startPoint and endPoint (F1, F2) is ellipse focuses
            //M1 is point which resides on ellipse arc and it's bottom right point of text frame size
            //Then ellipse width is F1M1 + F2M1
            var textSizeF1 = new CGPoint(0, size.Height / 2);
            var textSizeF2 = new CGPoint(size.Width, size.Height / 2);
            var textSizeM1 = new CGPoint(size.Width, size.Height);

            var F1M1 = Math.Sqrt(Math.Pow(textSizeM1.X - textSizeF1.X, 2) + Math.Pow(textSizeM1.Y - textSizeF1.Y, 2));
            var F2M1 = Math.Sqrt(Math.Pow(textSizeM1.X - textSizeF2.X, 2) + Math.Pow(textSizeM1.Y - textSizeF2.Y, 2));
            var ct = F1M1 + F2M1;
            return ct;
        }
    }
}
