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

    /// <summary>
    /// Display property name.<br/><br/>
    /// <example>
    /// For example, you want to use a list of <see cref="KeyValuePair{TKey, TValue}"/> as options for the dropdown,
    /// where Key is the displayed string for an option.
    /// <br/> Then set the folowing properties to:
    /// <code>
    ///     DisplayMemberPath = "Key"
    ///     SelectedValuePath = "Value"
    /// </code>
    /// </example>
    /// </summary>
    public string? DisplayMemberPath { get; set; }

    /// <summary>
    /// Value property name.<br/><br/>
    /// <example>
    /// For example, you want to use a list of <see cref="KeyValuePair{TKey, TValue}"/> as options for the dropdown,
    /// where Key is the displayed string for an option.
    /// <br/> Then set the folowing properties to:
    /// <code>
    ///     DisplayMemberPath = "Key"
    ///     SelectedValuePath = "Value"
    /// </code>
    /// </example>
    /// </summary>
    public string? SelectedValuePath { get; set; }

    /// <summary>
    /// Default selected item index
    /// </summary>
    public int? DefaultItemIndex { get; set; }

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
