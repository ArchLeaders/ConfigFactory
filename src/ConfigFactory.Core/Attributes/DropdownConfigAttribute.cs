using System.Collections;

namespace ConfigFactory.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DropdownConfigAttribute : Attribute
{
    public string[] ItemsSource { get; set; } = Array.Empty<string>();

    /// <summary>
    /// The name of the function or method in the owning
    /// class to return an <see cref="IEnumerable{T}"/> of source elements
    /// </summary>
    public string? RuntimeItemsSourceMethodName { get; set; }

    public IEnumerable GetItemsSource(object? context)
    {
        if (RuntimeItemsSourceMethodName != null) {
            return context?.GetType().GetMethod(RuntimeItemsSourceMethodName)?.Invoke(context, null) as IEnumerable ?? ItemsSource;
        }

        return ItemsSource;
    }

    public DropdownConfigAttribute() { }
    public DropdownConfigAttribute(params string[] items)
    {
        ItemsSource = items;
    }
}
