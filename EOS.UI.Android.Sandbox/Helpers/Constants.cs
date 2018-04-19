using System.Collections.Generic;
using Android.Graphics;

namespace EOS.UI.Android.Sandbox.Helpers
{
    public class Constants
    {
        public static class Colors
        {
            public const string Empty = "empty";
            public const string Black = "black";
            public const string White = "white";
            public const string Gray = "gray";
            public const string Green = "green";
            public const string Blue = "blue";
            public const string Red = "red";
            public const string Yellow = "yellow";
            public const string Brown = "brown";

            public static Dictionary<string, Color> ColorsCollection = new Dictionary<string, Color>()
            {
                { Empty, Color.Transparent },
                { Black, Color.Black },
                { White, Color.White },
                { Gray, Color.Gray },
                { Green, Color.Green },
                { Blue, Color.Blue },
                { Red, Color.Red },
                { Yellow, Color.Yellow },
                { Brown, Color.Brown }
            };
        }

        public static class Fonts
        {
            public const string Empty = "empty";
            public const string Roboto = "Roboto";
            public const string Berkshireswash = "Berkshireswash";
            public const string Amita = "Amita";

            public static Dictionary<string, string> FontsCollection = new Dictionary<string, string>()
            {
                { Empty, string.Empty },
                { Roboto, "Fonts/Roboto.ttf" },
                { Berkshireswash, "Fonts/Berkshireswash.ttf" },
                { Amita, "Fonts/Amita.ttf" },
            };
        }

    }
}