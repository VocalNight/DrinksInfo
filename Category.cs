using Newtonsoft.Json;

namespace DrinksInfo {
    public class Category : IDrinksJson
    {
        public string strCategory { get; set; }

        public string GetName()
        {
            return this.strCategory;
        }

        public void ChangeName( string name )
        {
            this.strCategory = name;
        }
    }

    public class Categories
    {
        [JsonProperty("drinks")]
        public List<Category> CategoriesList { get; set; }
    }
}
