﻿using Avalonia.Platform.Storage;
using ConfigFactory.Core.Attributes;

namespace ConfigFactory.Avalonia.Helpers;

public sealed class BrowserDialog
{
    public static IStorageProvider? StorageProvider { get; set; }

    private static Dictionary<string, BrowseHistory> Stashed { get; set; } = new();

    private record BrowseHistory()
    {
        public IStorageFolder? OpenDirectory { get; set; }

        public IStorageFolder? SaveDirectory { get; set; }
    }

    public static IStorageFolder? LastOpenDirectory { get; set; }

    public static IStorageFolder? LastSaveDirectory { get; set; }

    private readonly BrowserMode _mode;
    private string? _title;
    private readonly string? _filter;
    private readonly string? _suggestedFileName;
    private readonly string? _instanceBrowserKey;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="title"></param>
    /// <param name="filter">Semicolon delimited list of file filters. (Syntax: <c>Yaml Files<see cref="char">:</see>*.yml<see cref="char">;</see>*.yaml<see cref="char">|</see>All Files<see cref="char">:</see>*.*</c>)</param>
    /// <param name="suggestedFileName"></param>
    /// <param name="instanceBrowserKey">Saves the last open/save directory as an instance mapped to the specified key</param>
    public BrowserDialog(BrowserMode mode, string? title = null, string? filter = null, string? suggestedFileName = null, string? instanceBrowserKey = null)
    {
        _mode = mode;
        _title = title;
        _filter = filter;
        _suggestedFileName = suggestedFileName;
        _instanceBrowserKey = instanceBrowserKey;

        if (instanceBrowserKey != null) {
            if (!Stashed.ContainsKey(instanceBrowserKey)) {
                Stashed.Add(instanceBrowserKey, new BrowseHistory());
            }
        }
    }

    /// <inheritdoc cref="ShowDialog(bool)"/>
    public async Task<string?> ShowDialog()
    {
        return (await ShowDialog(false))?.First();
    }

    /// <summary>
    /// Opens a new <see cref="IStorageProvider"/> dialog and returns the selected files/folders.
    /// </summary>
    /// <param name="allowMultiple"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>?> ShowDialog(bool allowMultiple)
    {
        if (StorageProvider == null) {
            throw new InvalidOperationException(
                "The public IBrowserDialog implementation does not work without " +
                "setting the static IStorageProvider property.");
        }

        _title ??= _mode.ToString().Replace("F", " F");

        object? result = _mode switch {
            BrowserMode.OpenFolder => await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions() {
                Title = _title,
                SuggestedStartLocation = GetLastDirectory(),
                AllowMultiple = allowMultiple
            }),
            BrowserMode.OpenFile => await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions() {
                Title = _title,
                SuggestedStartLocation = GetLastDirectory(),
                AllowMultiple = allowMultiple,
                FileTypeFilter = LoadFileBrowserFilter(_filter)
            }),
            BrowserMode.SaveFile => await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions() {
                Title = _title,
                SuggestedStartLocation = GetLastDirectory(),
                FileTypeChoices = LoadFileBrowserFilter(_filter),
                SuggestedFileName = _suggestedFileName
            }),
            _ => throw new NotImplementedException()
        };

        switch (result) {
            case IReadOnlyList<IStorageFolder> { Count: > 0 } folders:
                SetLastDirectory(folders[^1]);
                return folders.Select(folder => folder.Path.LocalPath);
            case IReadOnlyList<IStorageFile> { Count: > 0 } files:
                SetLastDirectory(await files[^1].GetParentAsync());
                return files.Select(file => file.Path.LocalPath);
            case IStorageFile file:
                SetLastDirectory(await file.GetParentAsync());
                return new string[1] {
                    file.Path.LocalPath
                };
            default:
                return null;
        }
    }

    private void SetLastDirectory(IStorageFolder? folder)
    {
        if (_mode == BrowserMode.SaveFile) {
            LastSaveDirectory = folder;
            if (_instanceBrowserKey != null) {
                Stashed[_instanceBrowserKey].SaveDirectory = folder;
            }
        }
        else {
            LastOpenDirectory = folder;
            if (_instanceBrowserKey != null) {
                Stashed[_instanceBrowserKey].OpenDirectory = folder;
            }
        }
    }

    private IStorageFolder? GetLastDirectory()
    {
        if (_mode == BrowserMode.SaveFile) {
            return _instanceBrowserKey is not null
                ? Stashed[_instanceBrowserKey].SaveDirectory
                : LastSaveDirectory;
        }

        return _instanceBrowserKey is not null
            ? Stashed[_instanceBrowserKey].OpenDirectory
            : LastOpenDirectory;
    }

    private static FilePickerFileType[] LoadFileBrowserFilter(string? filter = null)
    {
        if (filter != null) {
            try {
                string[] groups = filter.Split('|');
                var types = new FilePickerFileType[groups.Length];

                for (int i = 0; i < groups.Length; i++) {
                    string[] pair = groups[i].Split(':');
                    types[i] = new FilePickerFileType(pair[0]) {
                        Patterns = pair[1].Split(';')
                    };
                }

                return types;
            }
            catch {
                throw new FormatException(
                    $"Could not parse filter arguments '{filter}'.\n" +
                    $"Example: \"Yaml Files:*.yml;*.yaml|All Files:*.*\"."
                );
            }
        }

        return Array.Empty<FilePickerFileType>();
    }
}