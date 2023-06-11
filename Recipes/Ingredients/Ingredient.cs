namespace CookiesCookbook.Recipes.Ingredients;

/// <summary>
/// Class discribed Ingredients with ID, Name and preparation instructions.
/// </summary>
public abstract class Ingredient
{
    public abstract int Id { get; }
    public abstract string Name { get; }
    public virtual string PreparationInstructions => "Add to other ingredients.";

    public override string ToString() => $"{Id}. {Name}";
}
