using Avalonia.Controls;
using Avalonia.Threading;
using LiveMarkdown.Avalonia;

namespace MahApps.IconPacksBrowser.Avalonia.Views;

public partial class AboutView : UserControl
{
    public AboutView()
    {
        InitializeComponent();
        
        Dispatcher.UIThread.Post(LoadAboutContent, DispatcherPriority.Background);
    }

    private void LoadAboutContent()
    {
        var markdownBuilder = new ObservableStringBuilder();
        MarkdownRenderer.MarkdownBuilder = markdownBuilder;

        markdownBuilder.Append(
"""
# About

## MahApps.Metro.IconPacks
The IconPacks library contains controls, markup extensions and converters to use these awesome icons with your applications in a simple way.

For more information visit the [project page](https://github.com/MahApps/MahApps.Metro.IconPacks).

## MahApps.Metro.IconPacks.Browser
This browser helps you to search for icons and use them in your applications. The source code of the browser is available on [GitHub](https://github.com/MahApps/MahApps.Metro.IconPacks.Browser).
The browser itself is released unter the [MIT License](https://github.com/MahApps/IconPacks.Browser/blob/main/LICENSE).

## Licenses
While the browser and the library is released under the MIT License, the icons themselves are released under their own licenses.

Please check the [IconPacks project page](https://github.com/MahApps/MahApps.Metro.IconPacks) for more information. 

> **TIP:** In the status bar of the browser you can open the project pages of the selected icon as well as look-up it's license.

## Special Thanks
Special thanks to all contributors of the IconPacks project. 

Also thanks to all auhtors of the used libraries:

- [Avalonia](https://avaloniaui.net)
- [AsyncAwaitBestPractices](https://github.com/TheCodeTraveler/AsyncAwaitBestPractices)
- [FluentAvalonia](https://github.com/amwx/FluentAvalonia)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/de-de/dotnet/communitytoolkit/mvvm/)
- [LiveMarkdown.Avalonia](https://github.com/DearVa/LiveMarkdown.Avalonia)
- [ReactiveUI](https://www.reactiveui.net)
- [SkiaSharp](https://github.com/mono/SkiaSharp)

""");
    }
}