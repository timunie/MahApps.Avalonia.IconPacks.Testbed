using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using DynamicData.Binding;
using IconPacks.Avalonia.BootstrapIcons;
using IconPacks.Avalonia.BoxIcons;
using IconPacks.Avalonia.BoxIcons2;
using IconPacks.Avalonia.CircumIcons;
using IconPacks.Avalonia.Codicons;
using IconPacks.Avalonia.Coolicons;
using IconPacks.Avalonia.Entypo;
using IconPacks.Avalonia.EvaIcons;
using IconPacks.Avalonia.FeatherIcons;
using IconPacks.Avalonia.FileIcons;
using IconPacks.Avalonia.Fontaudio;
using IconPacks.Avalonia.FontAwesome;
using IconPacks.Avalonia.FontAwesome5;
using IconPacks.Avalonia.FontAwesome6;
using IconPacks.Avalonia.Fontisto;
using IconPacks.Avalonia.ForkAwesome;
using IconPacks.Avalonia.GameIcons;
using IconPacks.Avalonia.Ionicons;
using IconPacks.Avalonia.JamIcons;
using IconPacks.Avalonia.KeyruneIcons;
using IconPacks.Avalonia.Lucide;
using IconPacks.Avalonia.Material;
using IconPacks.Avalonia.MaterialDesign;
using IconPacks.Avalonia.MaterialLight;
using IconPacks.Avalonia.MemoryIcons;
using IconPacks.Avalonia.Microns;
using IconPacks.Avalonia.MingCuteIcons;
using IconPacks.Avalonia.Modern;
using IconPacks.Avalonia.MynaUIIcons;
using IconPacks.Avalonia.Octicons;
using IconPacks.Avalonia.PhosphorIcons;
using IconPacks.Avalonia.PicolIcons;
using IconPacks.Avalonia.PixelartIcons;
using IconPacks.Avalonia.RadixIcons;
using IconPacks.Avalonia.RemixIcon;
using IconPacks.Avalonia.RPGAwesome;
using IconPacks.Avalonia.SimpleIcons;
using IconPacks.Avalonia.Typicons;
using IconPacks.Avalonia.Unicons;
using IconPacks.Avalonia.VaadinIcons;
using IconPacks.Avalonia.WeatherIcons;
using IconPacks.Avalonia.Zondicons;
using MahApps.IconPacksBrowser.Avalonia.Helper;
using ReactiveUI;

namespace MahApps.IconPacksBrowser.Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static MainViewModel Instance { get; } = new();

    public MainViewModel()
    {
        this.AppVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion!;
        SelectedNavigationItem = AvailableIconPacks[0];

        // Throttle text filter to avoid filtering on every keystroke
        var filterByText = this.WhenAnyValue(x => x.FilterText)
            .Throttle(TimeSpan.FromMilliseconds(300))
            .DistinctUntilChanged()
            .Select(FilterIconsByStringPredicate);

        // Icon pack filter doesn't need throttling (changes infrequently)
        var filterByIconPack = this.WhenAnyValue(x => x.SelectedIconPack)
            .DistinctUntilChanged()
            .Select(FilterIconsByTypePredicate);

        // Combine both filters into a single observable for better performance
        var combinedFilter = Observable.CombineLatest(
                filterByIconPack,
                filterByText,
                (packFilter, textFilter) => new Func<IIconViewModel, bool>(icon => 
                    packFilter(icon) && textFilter(icon)))
            .StartWith(_ => true);

        _iconsCache.Connect()
            .Filter(combinedFilter)
            .ObserveOn(RxApp.MainThreadScheduler)
            .SortAndBind(out _visibleIcons, 
                SortExpressionComparer<IIconViewModel>.Ascending(e => e.Identifier))
            .DisposeMany()
            .Subscribe();
        
        //LoadIconPacksAsync().SafeFireAndForget();

        AppVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
    }

    [ObservableProperty] public partial int TotalItems { get; set; }

    public async Task LoadIconPacksAsync()
    {
        var availableIconPacks = new List<(Type EnumType, Type IconPackType)>(
            [
                (typeof(PackIconBootstrapIconsKind), typeof(PackIconBootstrapIcons)),
                (typeof(PackIconBoxIcons2Kind), typeof(PackIconBoxIcons2)),
                (typeof(PackIconBoxIconsKind), typeof(PackIconBoxIcons)),
                (typeof(PackIconCircumIconsKind), typeof(PackIconCircumIcons)),
                (typeof(PackIconCodiconsKind), typeof(PackIconCodicons)),
                (typeof(PackIconCooliconsKind), typeof(PackIconCoolicons)),
                (typeof(PackIconEntypoKind), typeof(PackIconEntypo)),
                (typeof(PackIconEvaIconsKind), typeof(PackIconEvaIcons)),
                (typeof(PackIconFeatherIconsKind), typeof(PackIconFeatherIcons)),
                (typeof(PackIconFileIconsKind), typeof(PackIconFileIcons)),
                (typeof(PackIconFontaudioKind), typeof(PackIconFontaudio)),
                (typeof(PackIconFontAwesome5Kind), typeof(PackIconFontAwesome5)),
                (typeof(PackIconFontAwesome6Kind), typeof(PackIconFontAwesome6)),
                (typeof(PackIconFontAwesomeKind), typeof(PackIconFontAwesome)),
                (typeof(PackIconFontistoKind), typeof(PackIconFontisto)),
                (typeof(PackIconForkAwesomeKind), typeof(PackIconForkAwesome)),
                (typeof(PackIconGameIconsKind), typeof(PackIconGameIcons)),
                (typeof(PackIconIoniconsKind), typeof(PackIconIonicons)),
                (typeof(PackIconJamIconsKind), typeof(PackIconJamIcons)),
                (typeof(PackIconKeyruneIconsKind), typeof(PackIconKeyruneIcons)),
                (typeof(PackIconLucideKind), typeof(PackIconLucide)),
                (typeof(PackIconMaterialKind), typeof(PackIconMaterial)),
                (typeof(PackIconMaterialLightKind), typeof(PackIconMaterialLight)),
                (typeof(PackIconMaterialDesignKind), typeof(PackIconMaterialDesign)),
                (typeof(PackIconMemoryIconsKind), typeof(PackIconMemoryIcons)),
                (typeof(PackIconMicronsKind), typeof(PackIconMicrons)),
                (typeof(PackIconMingCuteIconsKind), typeof(PackIconMingCuteIcons)),
                (typeof(PackIconModernKind), typeof(PackIconModern)),
                (typeof(PackIconMynaUIIconsKind), typeof(PackIconMynaUIIcons)),
                (typeof(PackIconOcticonsKind), typeof(PackIconOcticons)),
                (typeof(PackIconPhosphorIconsKind), typeof(PackIconPhosphorIcons)),
                (typeof(PackIconPicolIconsKind), typeof(PackIconPicolIcons)),
                (typeof(PackIconPixelartIconsKind), typeof(PackIconPixelartIcons)),
                (typeof(PackIconRadixIconsKind), typeof(PackIconRadixIcons)),
                (typeof(PackIconRemixIconKind), typeof(PackIconRemixIcon)),
                (typeof(PackIconRPGAwesomeKind), typeof(PackIconRPGAwesome)),
                (typeof(PackIconSimpleIconsKind), typeof(PackIconSimpleIcons)),
                (typeof(PackIconTypiconsKind), typeof(PackIconTypicons)),
                (typeof(PackIconUniconsKind), typeof(PackIconUnicons)),
                (typeof(PackIconVaadinIconsKind), typeof(PackIconVaadinIcons)),
                (typeof(PackIconWeatherIconsKind), typeof(PackIconWeatherIcons)),
                (typeof(PackIconZondiconsKind), typeof(PackIconZondicons))
            ])
            .Select(tuple =>
            {
                var iconPack = new IconPackViewModel(tuple.EnumType, tuple.IconPackType);
                AvailableIconPacks.Add(new IconPackNavigationItemViewModel(iconPack));
                return iconPack;
            });

        var loadIconsTasks = availableIconPacks.Select(ip => ip.LoadIconsAsync(ip.EnumType, ip.PackType));
        _iconsCache.AddOrUpdate((await Task.WhenAll(loadIconsTasks)).SelectMany(x => x));
        
        TotalItems = _iconsCache.Count;
        SelectedIcon = SelectedIconPack?.Icons.FirstOrDefault() ?? _iconsCache.Items.FirstOrDefault();
    }

    /// <summary>
    /// Gets the navigation view items for all icon packs
    /// </summary>
    public ObservableCollection<NavigationItemViewModelBase> AvailableIconPacks { get; } =
    [
        new AllIconPacksNavigationItemViewModel(),
        new SeparatorNavigationItemViewModel()
    ];

    /// <summary>
    /// Gets a list of option items such as settings and about
    /// </summary>
    public List<NavigationItemViewModelBase> OptionNavigationItems { get; } =
    [
        new AboutNavigationItem(),
        new SettingsNavigationItem()
    ];

    private readonly SourceCache<IIconViewModel, string> _iconsCache = new(x => x.Identifier);

    private readonly ReadOnlyObservableCollection<IIconViewModel> _visibleIcons;

    /// <summary>
    /// Gets a list of visible items
    /// </summary>
    public ReadOnlyObservableCollection<IIconViewModel> VisibleIcons => _visibleIcons;


    /// <summary>
    /// Gets the selected IconPack-NavigationItem
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedIconPack))]
    public partial NavigationItemViewModelBase SelectedNavigationItem { get; set; }

    /// <summary>
    /// Gets the selected IconPack
    /// </summary>
    public IconPackViewModel? SelectedIconPack => SelectedNavigationItem.Tag as IconPackViewModel;

    /// <summary>
    /// Gets or sets the selected icon
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveAsSvgCommand))]
    [NotifyCanExecuteChangedFor(nameof(SaveAsBitmapCommand))]
    [NotifyCanExecuteChangedFor(nameof(SaveAsWpfCommand))]
    [NotifyCanExecuteChangedFor(nameof(SaveAsAvaloniaCommand))]
    public partial IIconViewModel? SelectedIcon { get; set; }

    /// <summary>
    /// Gets or sets the filter text
    /// </summary>
    [ObservableProperty]
    public partial string? FilterText { get; set; }

    partial void OnFilterTextChanged(string? value)
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            _filterItems = null;
            return;
        }
        
        var outer = value?.Split(['+', ',', ';', '&'], StringSplitOptions.RemoveEmptyEntries);
        string[][]? inner =
            outer?.Select(x => x.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => y.Trim().ToLowerInvariant())
                    .ToArray())
                .ToArray();

        _filterItems = inner;
    }

    private string[][]? _filterItems;

    /// <summary>
    /// Gets the App version of this Application
    /// </summary>
    public string? AppVersion { get; }


    private Func<IIconViewModel, bool> FilterIconsByStringPredicate(string? filterText) => icon =>
    {
        // we have no filter text, so this icon should be visible
        if (_filterItems is null)
            return true;

        // All outer groups must match (AND logic)
        return _filterItems.All(outerGroup =>
            // At least one term in the group must match (OR logic)
            outerGroup.Any(searchStr =>
                icon.FilterString.Contains(searchStr)));
    };

    private Func<IIconViewModel, bool> FilterIconsByTypePredicate(IconPackViewModel? selectedIconPack) => icon =>
    {
        return SelectedNavigationItem is AllIconPacksNavigationItemViewModel
               || icon.IconPackType == selectedIconPack?.PackType;
    };


    private async Task DoCopyTextToClipboard(string? text)
    {
        if (text != null)
        {
            await this.SetClipboardContentAsync(text);
        }
    }

    bool CanExport => SelectedIcon != null;

    [RelayCommand]
    private async Task LaunchUriAsync(string? uri)
    {
        await this.OpenUriAsync(uri);
    }

    [RelayCommand]
    public async Task CopyTextToClipboardAsync(string? text)
    {
        await DoCopyTextToClipboard(text);
    }

    [RelayCommand]
    public async Task CopyToClipboardTextAsync(IIconViewModel icon)
    {
        await DoCopyTextToClipboard(icon.CopyToClipboardText);
    }

    [RelayCommand]
    public async Task CopyToClipboardAsContentTextAsync(IIconViewModel icon)
    {
        await DoCopyTextToClipboard(icon.CopyToClipboardAsContentText);
    }

    [RelayCommand]
    public async Task CopyToClipboardAsGeometryTextAsync(IIconViewModel icon)
    {
        await DoCopyTextToClipboard(icon.CopyToClipboardAsGeometryText);
    }

    [RelayCommand]
    public async Task CopyToClipboardAsPathIconTextAsync(IIconViewModel icon)
    {
        await DoCopyTextToClipboard(icon.CopyToClipboardAsPathIconText);
    }

    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task SaveAsSvgAsync(IIconViewModel icon)
    {
        await ExportHelper.SaveAsSvgAsync(icon);
    }

    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task SaveAsBitmapAsync(IIconViewModel icon)
    {
        await ExportHelper.SaveAsBitmapAsync(icon);
    }
    
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task SaveAsWpfAsync(IIconViewModel icon)
    {
        await ExportHelper.SaveAsWpfXamlAsync(icon);
    }
    
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task SaveAsAvaloniaAsync(IIconViewModel icon)
    {
        await ExportHelper.SaveAsAvaloniaXamlAsync(icon);
    }
}