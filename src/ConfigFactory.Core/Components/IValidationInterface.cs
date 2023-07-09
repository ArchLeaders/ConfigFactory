using System.Reflection;

namespace ConfigFactory.Core.Components;

public interface IValidationInterface
{
    public void SetValidationColor(PropertyInfo property, string hexColorCode);
}
