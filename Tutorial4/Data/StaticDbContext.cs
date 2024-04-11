namespace Tutorial4.Data;

public static class StaticDbContext
{
    public static List<Animal> Animals { get; set; } = new List<Animal>();
    public static List<Visit> Visits { get; set; } = new List<Visit>();
}
