using System.Collections.Generic;

namespace UIFrameworks.Shared.Themes.Interfaces
{
    public interface ITheme
    {
        Dictionary<string, object> ThemeValues { get; }
    }
}
