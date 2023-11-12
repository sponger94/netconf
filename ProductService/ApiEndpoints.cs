namespace ProductService;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Products
    {
        private const string Base = $"{ApiBase}/products";

        public const string Get = $"{Base}";
        public const string GetById = $"{Base}/{{id}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}";
        public const string Delete = $"{Base}/{{id}}";
    }
    
    // public static class ProductsTest
    // {
    //
    //     public const string Get = $"{Base}";
    //     public const string GetById = $"{Base}/{{id}}";
    //     public const string Create = $"{Base}";
    //     public const string Update = $"{Base}";
    //     public const string Delete = $"{Base}/{{id}}";
    // }

    public static class Categories
    {
        private const string Base = $"{ApiBase}/categories";
        
        public const string Get = $"{Base}";
        public const string GetById = $"{Base}/{{id}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}";
        public const string Delete = $"{Base}/{{id}}";
    }
}