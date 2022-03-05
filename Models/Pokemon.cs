public class Pokemon{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Habitat { get; private set; }
    public bool IsLegendary { get; private set; }

    public Pokemon(string name, string description, string habitat, bool isLegendary){
        Name = name;
        Description = description;
        Habitat = habitat;
        IsLegendary = isLegendary;
    }
}