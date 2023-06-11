using CookiesCookbook.Recipes;
using CookiesCookbook.Recipes.Ingredients;
using System.Text.Json;

const FileFormat Format = FileFormat.Txt;

IStringsRepository stringsRepository = Format == FileFormat.Json ?
    new StringsJsonRepository() :
    new StringsTextualRepository();

const string Filename = "recipes";
var fileMetadata = new FileMetadata(Filename, Format);

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(stringsRepository, ingredientsRegister),
    new RecipesConsoleUserInteraction(ingredientsRegister)); // Create an instance of the class using the constructor below. The way to communicate with user. In the future we can change it.

cookiesRecipesApp.Run(fileMetadata.ToPath()); // File name for storing recipes.

/// <summary>
/// Build a file path from the file's name and the file gotmat enum.
/// </summary>
public class FileMetadata
{
    public string Name { get; set; }

    public FileFormat Format { get; }

    public FileMetadata(string name, FileFormat format)
    {
        Name = name;
        Format = format;
    }

    public string ToPath() => $"{Name}.{Format.AsFileExtension()}";
}
/// <summary>
/// File format extension.
/// </summary>
public static class FileFormatExtensions
{
    public static string AsFileExtension(this FileFormat fileFormat) =>
        fileFormat == FileFormat.Json ? "json" : "txt";
}

/// <summary>
/// File formats for the saving recipes.
/// </summary>
public enum FileFormat
{
    Json,
    Txt
}

public class CookiesRecipesApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    /// <summary>
    /// Inicialise filds in class.
    /// </summary>
    /// <param name="recpesRepository">Recepies storage.</param>
    /// <param name="recpesUserInteraction">Ineraction with recepies. Like display, print and so on.</param>
    public CookiesRecipesApp(IRecipesRepository recipesRepository, IRecipesUserInteraction recipesUserInteraction) // It will be used when we create an instance of the class.
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction; // Creating deppendency injection. Class is given the dependencies it need. It doesn't create them itself.
    }

    /// <summary>
    /// Main program method.
    /// </summary>
    public void Run(string filePath)
    {
        var allRecipes = _recipesRepository.Read(filePath); // Step 1: Reading all recipes. TODO: make filePath.
        _recipesUserInteraction.PrintExistingRecipes(allRecipes); // Step 2: Display on the screen all recepies.
        _recipesUserInteraction.PromtToCreateRecipe(); // Step 3: Prompting the user to create recipy.
        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser(); //Step 4: // Reading ingredirnts from user.

        if (ingredients.Count() > 0) // Check: is user selected any ingredients?
        {
            var recipe = new Recipe(ingredients); // Using ingrediens for creating recipe.
            allRecipes.Add(recipe); // Add recepe in recipes list.
            _recipesRepository.Write(filePath, allRecipes); // Write recipes to a file.
            _recipesUserInteraction.ShowMessage("Recipe added."); //Show messade about adding.
            _recipesUserInteraction.ShowMessage(recipe.ToString()); // Show recepe.
        }
        else // Message if recepies are not added.
        {
            _recipesUserInteraction.ShowMessage("No ingredients has been selected. Recipe will not be saved.");
        }

        _recipesUserInteraction.Exit(); //Exit
    }
}

/// <summary>
/// Declares whst methods are needed to communicate with a user.
/// </summary>
public interface IRecipesUserInteraction
{
    /// <summary>
    /// Show the message
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message);

    /// <summary>
    /// Exit from program
    /// </summary>
    public void Exit();

    /// <summary>
    /// Display all recepies.
    /// </summary>
    /// <param name="allRecipes">List of all created  recepies.</param>
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);

    /// <summary>
    /// Prompt to creating recipes.
    /// </summary>
    /// <returns></returns>
    void PromtToCreateRecipe();

    /// <summary>
    /// Infing item in collection by Id.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Ingredient> ReadIngredientsFromUser();
}

public interface IIngredientsRegister
{
    IEnumerable<Ingredient> All { get; }

    Ingredient GetById(int id);
}

/// <summary>
/// Base of all ingredients.
/// </summary>
public class IngredientsRegister : IIngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>()
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };

    /// <summary>
    /// Get recipe ID
    /// </summary>
    /// <param name="id">recipe ID</param>
    /// <returns>ingredient from All(Base of all ingredients) or null if it cant find</returns>
    public Ingredient GetById(int id)
    {
        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
            {
                return ingredient;
            }
        }

        return null;
    }
}

/// <summary>
/// Cosole user interface.
/// </summary>
public class RecipesConsoleUserInteraction : IRecipesUserInteraction // Dependency Inversion Principle (SOLID). This type should depend on abstractions, not on concrete.
{
    private readonly IIngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IIngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }

    /// <summary>
    /// Show the message
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    /// Exit from program
    /// </summary>
    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    /// <summary>
    /// Display all recepies.
    /// </summary>
    /// <param name="allRecipes">List of all created  recepies.</param>
    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if (allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);
            var counter = 1;

            foreach (var recipe in allRecipes)
            {
                Console.WriteLine($"*****{counter}*****");
                Console.WriteLine(recipe);
                Console.WriteLine();
                counter++;
            }
        }
    }

    /// <summary>
    /// Prompt to creating recipes.
    /// </summary>
    /// <returns>Print all ingredients.</returns>
    public void PromtToCreateRecipe()
    {
        Console.WriteLine("Create a newcookie recipe! Available ingredients are:");
        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    /// <summary>
    /// User select ingredients or exit if he push any key.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        bool shallStop = false;

        var ingredients = new List<Ingredient>();

        while (!shallStop)
        {
            Console.WriteLine("Add an ingredient by itsID or type anything else if finished.");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetById(id);
                if (selectedIngredient is not null)
                {
                    ingredients.Add(selectedIngredient);
                }
            }
            else
            {
                shallStop = true; //Exit
            }
        }

        return ingredients;
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filePath);
    void Write(string filePath, List<Recipe> allRecipes);
}

/// <summary>
/// All recirpes repository.
/// </summary>
public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IIngredientsRegister _ingredientsRegister;
    private const string Separator = ",";

    public RecipesRepository(IStringsRepository stringsRepository, IIngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    /// <summary>
    /// Read the Recpies from the file.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public List<Recipe> Read(string filePath)
    {
        List<string> recipesFromFile = _stringsRepository.Read(filePath);
        var recipes = new List<Recipe>();

        foreach (var recipeFromFile in recipesFromFile)
        {
            var recipe = RecipeFromString(recipeFromFile);
            recipes.Add(recipe);
        }

        return recipes;
    }

    /// <summary>
    /// This method accept the string with ingredirnt IDs as a parameter an use them to build a recipe object.
    /// </summary>
    /// <param name="recipeFromFile"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private Recipe RecipeFromString(string recipeFromFile)
    {
        var textualIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();

        foreach (var textualId in textualIds)
        {
            var id = int.Parse(textualId);
            var ingredient = _ingredientsRegister.GetById(id);
            ingredients.Add(ingredient);
        }

        return new Recipe(ingredients);
    }

    /// <summary>
    /// Make recipes list (IDs are saparated by comma.)
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="allRecipes"></param>
    public void Write(string filePath, List<Recipe> allRecipes)
    {
        var recipesAsStrings = new List<string>();
        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAsStrings.Add(string.Join(Separator, allIds));
        }
        _stringsRepository.Write(filePath, recipesAsStrings);
    }
}

public interface IStringsRepository
{
    List<string> Read(string filePath);
    void Write(string filePath, List<string> strings);
}

/// <summary>
/// read/write recepies from/to text file.
/// </summary>
public class StringsTextualRepository : IStringsRepository
{
    private static readonly string Separator = Environment.NewLine;


    public List<string> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContents = File.ReadAllText(filePath);
            return fileContents.Split(Separator).ToList(); 
        }

        return new List<string>();
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, string.Join(Separator, strings));
    }
}

/// <summary>
/// read/write recepies from/to Json file.
/// </summary>
public class StringsJsonRepository : IStringsRepository
{
    public List<string> Read(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileContents = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<string>>(fileContents);
        }

        return new List<string>();
    }

    public void Write(string filePath, List<string> strings)
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(strings));
    }
}