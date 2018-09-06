using System;
using Foundation;

namespace EOS.UI.iOS.Extensions
{
    internal static class AttributedStringExtension
    {
        /// <summary>
        /// Changes the attribute in attributed string.
        /// </summary>
        /// <returns>The attribute.</returns>
        /// <param name="attributedString">Attributed string.</param>
        /// <param name="attributeName">Attribute name.</param>
        /// <param name="attributeValue">Attribute value.</param>
        public static NSMutableAttributedString ChangeAttribute(this NSAttributedString attributedString, NSString attributeName, NSObject attributeValue)
        {
            var mutableAttributedString = new NSMutableAttributedString(attributedString);
            mutableAttributedString.AddAttribute(attributeName, attributeValue, new NSRange(0, mutableAttributedString.Length));
            return mutableAttributedString;
        }
    }
}
