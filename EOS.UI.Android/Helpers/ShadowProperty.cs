﻿using System;
using Android.Graphics;
using Java.IO;

namespace EOS.UI.Android.Helpers
{
    public class ShadowProperty: Java.Lang.Object, ISerializable
    {
        public const int ALL = 0x1111;
        public const int LEFT = 0x0001;
        public const int TOP = 0x0010;
        public const int RIGHT = 0x0100;
        public const int BOTTOM = 0x1000;

        private int shadowColor;
        private int shadowRadius;
        private int shadowDx;
        private int shadowDy;
        private int shadowSide = ALL;
        
        public int getShadowSide()
        {
            return shadowSide;
        }

        public ShadowProperty setShadowSide(int shadowSide)
        {
            this.shadowSide = shadowSide;
            return this;
        }

        public int getShadowOffset()
        {
            return getShadowOffsetHalf() * 2;
        }

        public int getShadowOffsetHalf()
        {
            return 0 >= shadowRadius ? 0 : Math.Max(shadowDx, shadowDy) + shadowRadius;
        }

        public Color getShadowColor()
        {
            return new Color(shadowColor);
        }

        public ShadowProperty setShadowColor(int shadowColor)
        {
            this.shadowColor = shadowColor;
            return this;
        }

        public int getShadowRadius()
        {
            return shadowRadius;
        }

        public ShadowProperty setShadowRadius(int shadowRadius)
        {
            this.shadowRadius = shadowRadius;
            return this;
        }

        public int getShadowDx()
        {
            return shadowDx;
        }

        public ShadowProperty setShadowDx(int shadowDx)
        {
            this.shadowDx = shadowDx;
            return this;
        }

        public int getShadowDy()
        {
            return shadowDy;
        }

        public ShadowProperty setShadowDy(int shadowDy)
        {
            this.shadowDy = shadowDy;
            return this;
        }
    }
}
