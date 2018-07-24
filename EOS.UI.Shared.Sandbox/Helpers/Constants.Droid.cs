#if __IOS__
#else
using System;
using System.Collections.Generic;
using Android.Graphics;

namespace EOS.UI.Shared.Sandbox.Helpers
{
    public static partial class Constants
    {
        public static class Colors
        {
            public static readonly Dictionary<string, Color> ColorsCollection = new Dictionary<string, Color>()
            {
                { string.Empty, Color.Transparent },
                { "Black", Color.Black },
                { "White", Color.White },
                { "Gray", Color.Gray },
                { "Green", Color.Green },
                { "Blue", Color.Blue },
                { "Red", Color.Red },
                { "Yellow", Color.Yellow },
                { "Brown", Color.Brown }
            };
        }

        public static class Fonts
        {
            public const string Roboto = "Roboto";
            public const string Berkshireswash = "Berkshireswash";
            public const string Amita = "Amita";
            public const string AcademyEngraved = "AcademyEngraved";

            public static readonly Dictionary<string, string> FontsCollection = new Dictionary<string, string>()
            {
                { string.Empty, string.Empty },
                { Roboto, "Fonts/Roboto.ttf" },
                { Berkshireswash, "Fonts/Berkshireswash.ttf" },
                { Amita, "Fonts/Amita.ttf" },
                { AcademyEngraved, "Fonts/academyEngraved.ttf" },
            };
        }
    }
}
#endif
