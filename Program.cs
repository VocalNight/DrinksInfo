using DrinksInfo;
using Newtonsoft.Json;
using RestSharp;



List<Category> list = GetDrinkCategories();

Console.Write("Choose Category: ");

string? choice = Console.ReadLine();

string drink = choice switch
{
    "1" => filterString(list[0].GetName()),
    "2" => filterString(list[1].GetName()),
    "3" => filterString(list[2].GetName()),
    "4" => filterString(list[3].GetName()),
    "5" => filterString(list[4].GetName()),
    "6" => filterString(list[5].GetName()),
    "7" => filterString(list[6].GetName()),
    "8" => filterString(list[7].GetName()),
    "9" => filterString(list[8].GetName()),
    "10" => filterString(list[9].GetName()),
    "11" => filterString(list[10].GetName()),
    _ => "This category dosn't exist, select a visible one"
};

if (drink == "This category dosn't exist, select a visible one")
{
    Console.WriteLine(drink);
} else
{
    bool selectingDrink = true;
    int index = 0;

    List<Drink> drinksList = getDrinks(drink);
    List<List<Drink >> separatedDrinks = Split(drinksList);

    foreach (List<Drink> drinks in separatedDrinks)
    {
        EditList(drinks);
    }

    while (selectingDrink)
    {

        CreateTableEngine.ShowTable(separatedDrinks[index], drink);
        Console.WriteLine($"Page {index + 1} of {separatedDrinks.Count()}");

        Console.WriteLine("Choose a drink with the numpad or press left and right to change pages");

        ConsoleKey drinkChoice = Console.ReadKey().Key;
        Console.Clear();

        if (drinkChoice == ConsoleKey.LeftArrow && index > 0)
        {
            index--;
        } else if (drinkChoice == ConsoleKey.RightArrow && index < separatedDrinks.Count() - 1)
        {
            index++;
        }

        string choosenDrink = drinkChoice switch
        {
            ConsoleKey.NumPad1 => separatedDrinks[index][0].GetName(),
            ConsoleKey.NumPad2 => separatedDrinks[index][1].GetName(),
            ConsoleKey.NumPad3 => separatedDrinks[index][2].GetName(),
            ConsoleKey.NumPad4 => separatedDrinks[index][3].GetName(),
            ConsoleKey.NumPad5 => separatedDrinks[index][4].GetName(),
            ConsoleKey.NumPad6 => separatedDrinks[index][5].GetName(),
            ConsoleKey.NumPad7 => separatedDrinks[index][6].GetName(),
            ConsoleKey.NumPad8 => separatedDrinks[index][7].GetName(),
            ConsoleKey.NumPad9 => separatedDrinks[index][8].GetName(),
        };
    }


}

List<Drink> getDrinks(string category)
{
    var jsonClient = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
    var request = new RestRequest($"filter.php?c={category}");

    var response = jsonClient.ExecuteAsync(request);

    if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
    {

        string rawResponse = response.Result.Content;
        var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

         List<Drink> drinks = serialize.DrinksList;

        return drinks;
    }

    return null;
}
 
string filterString(string text)
{
    return text.Remove(0, 4);
}


List<List<T>> Split<T>( IList<T> source )
{
    return source
        .Select(( x, i ) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / 9)
        .Select(x => x.Select(v => v.Value).ToList())
        .ToList();
}

List<Category> GetDrinkCategories() {

    var jsonClient = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
    var request = new RestRequest("list.php?c=list");
    var response = jsonClient.ExecuteAsync(request);

    if (response.Result.StatusCode == System.Net.HttpStatusCode.OK) {

        string rawResponse = response.Result.Content;
        var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

        List<Category> categories = serialize.CategoriesList;

        CreateTableEngine.ShowTable(EditList(categories), "Categories");

        return categories;
    }
    return null;
}

List<T> EditList<T>(List<T> list) where T : IDrinksJson
{
    int enumeration = 0;

    list.ForEach(category =>
    {
        enumeration++;
        string newName = $"{enumeration} - " + category.GetName();
        category.ChangeName(newName);
    });

    return list;
}