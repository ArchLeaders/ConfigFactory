using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ConfigFactory.Avalonia.Controls;

public class ConfigItem : ContentControl
{
    public static readonly StyledProperty<string> HeaderProperty =
		AvaloniaProperty.Register<ConfigItem, string>(nameof(Header));

    public static readonly StyledProperty<string> DescriptionProperty =
		AvaloniaProperty.Register<ConfigItem, string>(nameof(Description));

    public static readonly StyledProperty<Thickness> InnerMarginProperty =
		AvaloniaProperty.Register<ConfigItem, Thickness>(nameof(InnerMargin), new Thickness(5));

    public static readonly StyledProperty<GridLength> ContentColumnProperty =
		AvaloniaProperty.Register<ConfigItem, GridLength>(nameof(ContentColumn), new GridLength(1, GridUnitType.Star));

    public static readonly StyledProperty<BoxShadows> BoxShadowProperty =
		AvaloniaProperty.Register<ConfigItem, BoxShadows>(nameof(BoxShadow));

    public static readonly StyledProperty<IBrush> ValidationBrushProperty =
		AvaloniaProperty.Register<ConfigItem, IBrush>(nameof(ValidationBrush), defaultValue: Brushes.Transparent);

    public string Header {
		get => GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}

	public string Description {
		get => GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}

	public Thickness InnerMargin {
		get => GetValue(InnerMarginProperty);
		set => SetValue(InnerMarginProperty, value);
	}

	public GridLength ContentColumn {
		get => GetValue(ContentColumnProperty);
		set => SetValue(ContentColumnProperty, value);
	}

	public BoxShadows BoxShadow {
		get => GetValue(BoxShadowProperty);
		set => SetValue(BoxShadowProperty, value);
	}

	public IBrush ValidationBrush {
		get => GetValue(ValidationBrushProperty);
		set => SetValue(ValidationBrushProperty, value);
	}
}
